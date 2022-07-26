using System.Security.Cryptography;
using System.Text.RegularExpressions;
using AutoMapper;
using ems_backend.Data;
using ems_backend.Entities;
using ems_backend.Models;
using ems_backend.Services;
using ems_backend.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace ems_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly EMSContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _auth;
        private readonly IMailService _mailer;
        public AuthController(EMSContext context, IMapper mapper, IAuthService auth, IMailService mailer)
        {
            _context = context;
            _mapper = mapper;
            _auth = auth;
            _mailer = mailer;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(RegisterRequestModel request)
        {
            if (_context.Users.Any(x => x.MailAddress == request.MailAddress
                                        || x.Username == request.Username))
            {
                return BadRequest("User already exists");
            }

            // CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            _auth.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = _mapper.Map<User>(request);
            user.VerificationToken = _auth.CreateRandomToken();
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            string url = $@"https://localhost:7072/api/auth/verify?token={user.VerificationToken}";


            // Send confirmation mail
            string messageBody = string.Format(@"<div style='text-align-center;'>
                                <h1>Click the below link to verify your account</h1>
                                <form method='post' action='{0}' style='display: inline;'>
                                    <button type = 'submit' style = '
                                        display: block;
                                        text-align: center;
                                        font-weight: boold;
                                        background-color: #008CBA;
                                        font-size: 16px;
                                        border-radius: 10px;
                                        color: #ffffff;
                                        cursor: pointer;
                                        width: 100%;
                                        padding: 10px;
                                    '> Confirm </button>
                                </form>
                                </div>
            ", url);

            _mailer.Send(user.MailAddress, "no-reply", messageBody);

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult Login(LoginRequestModel request)
        {
            var user = _context.Users.FirstOrDefault(x =>
                x.Username == request.UsernameOrMailAddress ||
                x.MailAddress == request.UsernameOrMailAddress
            );

            if (user == null)
            {
                return BadRequest("User not found!");
            }

            if (user.PasswordSalt != null && !_auth.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is incorrect!");
            }

            if (user.VerifiedAt == DateTime.MinValue)
            {
                return BadRequest("Not verified");
            }

            string accessToken = _auth.CreateAccessToken(user);

            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64)),
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(7)
            };

            setRefreshToken(user, refreshToken);
            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };

            Response.Cookies.Append("refresh-token", refreshToken.Token, cookieOptions);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(new { accessToken = accessToken });
        }

        [HttpPost("refresh-token"), Authorize()]
        public ActionResult<string> RefreshToken()
        {
            var refreshToken = Request.Cookies["refresh-token"];
            User currentUser;

            Console.WriteLine(GetUser());

            try
            {
                currentUser = _context.Users.First(x => x.Username == GetUser());
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }

            if (currentUser.RefreshToken != refreshToken)
            {
                return Unauthorized("Invalid refresh token");
            }
            else if (currentUser.RefreshExpires < DateTime.Now)
            {
                return Unauthorized("Refresh token expired");
            }

            string token = _auth.CreateAccessToken(currentUser);
            var newRefreshToken = GenerateRefreshToken();

            setRefreshToken(currentUser, newRefreshToken);

            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };

            Response.Cookies.Append("refresh-token", newRefreshToken.Token, cookieOptions);

            return Ok(token);
        }

        [HttpPost("verify")]
        public IActionResult Verify(string token)
        {
            var user = _context.Users.FirstOrDefault(x => x.VerificationToken == token);

            if (user == null)
            {
                return BadRequest("Invalid token");
            }

            if (user.VerifiedAt != DateTime.MinValue)
                return BadRequest("User already verified");

            user.VerifiedAt = DateTime.Now;
            _context.SaveChanges();

            return Ok("User verified!");
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(string UsernameOrMailAddress)
        {
            var user = _context.Users.FirstOrDefault(x =>
                x.Username == UsernameOrMailAddress ||
                x.MailAddress == UsernameOrMailAddress
            );

            if (user == null)
            {
                return BadRequest("User not found!");
            }

            // user.PasswordResetToken = _auth.CreateRandomToken();
            // user.ResetTokenExpires  = DateTime.Now.AddDays(1);

            user.Otp = _auth.GenerateOtp();
            user.OtpExpires = DateTime.Now.AddMinutes(10);

            string messageBody = string.Format(@"
                <div>
                    <h1>Account Password Reset</h1>
                    <h3>OTP: {0}</h3>
                    <h3>The OTP is valid for 10 minutes</h3>
                </div>
            ", user.Otp);

            _mailer.Send(user.MailAddress, "no-reply", messageBody);

            _context.SaveChanges();

            return Ok();
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword(ResetPasswordModel request)
        {
            var user = _context.Users.FirstOrDefault(x =>
                x.Otp == request.Otp
            );

            if (user == null || user.OtpExpires < DateTime.Now)
            {
                return BadRequest("Invalid OTP");
            }

            _auth.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user.Otp = null;
            user.OtpExpires = DateTime.MinValue;

            _context.SaveChanges();

            return Ok("Password reset succesfully");
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = _auth.CreateRandomToken(),
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(7)
            };


            return refreshToken;
        }

        private void setRefreshToken(User user, RefreshToken refreshToken)
        {
            user.RefreshToken = refreshToken.Token;
            user.RefreshCreated = refreshToken.Created;
            user.RefreshExpires = refreshToken.Expires;
        }

        private string GetUser()
        {
            string token;
            try
            {
                token = Request.Headers["Authorization"].First().Split(' ').Last();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            var userId = jwtSecurityToken.Claims.First(x => x.Type.Split('/').Last() == "nameidentifier").Value;

            return _context.Users.First(x => x.UserId == Convert.ToInt32(userId)).Username ?? "";
        }
    }
}