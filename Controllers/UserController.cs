using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ems_backend.Data;
using ems_backend.Entities;
using ems_backend.Models;
using AutoMapper;
using ems_backend.Repositories;

namespace ems_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EMSContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;

        public UserController(EMSContext context, IMapper mapper, IUserRepository userRepo)
        {
            _context = context;
            _mapper = mapper;
            _userRepo = userRepo;
        }

        // GET: api/User
        [HttpGet]
        public ActionResult<IEnumerable<UserResponseModel>> GetUsers()
        {
            try
            {
                var userList = _userRepo.GetAll();
                return Ok(userList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public ActionResult<UserResponseModel> GetUser(int id)
        {
            try
            {
                var user = _userRepo.GetById(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, UserRequestModel model)
        {
            try
            {
                _userRepo.Update(id, model);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/User
        // [HttpPost]
        // public ActionResult<UserResponseModel> PostUser(UserRequestModel model)
        // {
        //     try
        //     {
        //         Dictionary<int, UserResponseModel> response = _userRepo.Create(model);
        //         int userId = response.Keys.First();
        //         return CreatedAtAction("GetUser", new { id = userId }, response);
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userRepo.Delete(id);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
