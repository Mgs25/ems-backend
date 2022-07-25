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
using System.Text.Json;
using ems_backend.Repositories;

namespace ems_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly EMSContext _context;
        private readonly IMapper _mapper;
        private readonly IEnrollmentRepository _enrollmentRepo;
        private readonly IUserRepository _userRepo;

        public EnrollmentController(EMSContext context, IMapper mapper, IEnrollmentRepository enrollmentRepo, IUserRepository userRepo)
        {
            _context = context;
            _mapper = mapper;
            _enrollmentRepo = enrollmentRepo;
            _userRepo = userRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EnrollmentResponseModel>> GetEnrollments()
        {
            try
            {
                var enrollmentList = _enrollmentRepo.GetAll();
                return Ok(enrollmentList);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet("{id}")]
        public ActionResult<EnrollmentResponseModel> GetEnrollment(int id)
        {
            try
            {
                var enrollment = _enrollmentRepo.GetById(id);
                return Ok(enrollment);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<EnrollmentResponseModel> PostEnrollment(EnrollmentRequestModel model)
        {
            try
            {
                Dictionary<int, EnrollmentResponseModel> response = _enrollmentRepo.Create(model);
                int enrollmentId = response.Keys.First();
                return CreatedAtAction("GetEnrollment", new { id = enrollmentId }, response[enrollmentId]);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEnrollment(int id)
        {
            try
            {
                _enrollmentRepo.Delete(id);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetEventsByUserId/{id}")]
        public ActionResult<IEnumerable<Event>> GetEventsByUserId(int id)
        {
            try
            {
                IEnumerable<Event> events = _enrollmentRepo.GetEventsByUserID(id);
                return Ok(events);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private bool EnrollmentExists(int id)
        {
            return (_context.Enrollments?.Any(e => e.EnrollmentId == id)).GetValueOrDefault();
        }
    }
}
