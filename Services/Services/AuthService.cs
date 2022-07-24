using System.Security.Cryptography;
using System.Security.Claims;
using ems_backend.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ems_backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        public string GenerateOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            return otp.ToString();
        }

        public string CreateAccessToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("userId", user.UserId.ToString()),
                new Claim("Role", user.Role.ToString() ?? "user")
            };

            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("Secret:Token").Value
                )
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwtString = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtString;
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}