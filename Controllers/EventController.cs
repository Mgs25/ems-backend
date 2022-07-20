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
    public class EventController : ControllerBase
    {
        private readonly EMSContext _context;
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepo;

        public EventController(EMSContext context, IMapper mapper, IEventRepository eventRepo)
        {
            _context = context;
            _mapper = mapper;
            _eventRepo = eventRepo;
        }

        // GET: api/Events
        [HttpGet]
        public ActionResult<IEnumerable<EventResponseModel>> GetEvents()
        {
            try
            {
                var eventList = _eventRepo.GetAll();
                return Ok(eventList);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public ActionResult<EventResponseModel> GetEvent(int id)
        {
            try
            {
                var @event = _eventRepo.GetById(id);
                return Ok(@event);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut("{id}")]
        public IActionResult PutEvent(int id, EventRequestModel model)
        {
            try
            {
                _eventRepo.Update(id, model);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<EventResponseModel> PostEvent(EventRequestModel model)
        {
            try
            {
                Dictionary<int, EventResponseModel> response = _eventRepo.Create(model);
                int eventId = response.Keys.First();
                return CreatedAtAction("GetEvent", new { id = eventId }, response[eventId]);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            try
            {
                _eventRepo.Delete(id);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
