using AutoMapper;
using ems_backend.Data;
using ems_backend.Entities;
using ems_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ems_backend.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EMSContext _context;
        private readonly IMapper _mapper;
        public EventRepository(EMSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<EventResponseModel> GetAll()
        {
            if (_context.Events == null)
            {
                throw new Exception("Not found");
            }
            // return await _context.Events.ToListAsync();
            List<EventResponseModel> eventList = new List<EventResponseModel>();

            foreach (var @event in _context.Events.ToList())
            {
                eventList.Add(_mapper.Map<EventResponseModel>(@event));
            }

            return eventList;
        }

        public EventResponseModel GetById(int id)
        {
            if (_context.Events == null)
            {
                throw new Exception("Not found");
            }

            var @event = _context.Events.Find(id);

            if (@event == null)
                throw new Exception("Not found");

            var mappedEvent = _mapper.Map<EventResponseModel>(@event);

            return mappedEvent;
        }

        public void Update(int id, EventRequestModel model)
        {
            var @event = _mapper.Map<Event>(model);
            @event.EventId = id;

            _context.Events.Update(@event);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    throw new Exception("Not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public Dictionary<int, EventResponseModel> Create(EventRequestModel model)
        {
            if (_context.Events == null)
            {
                throw new Exception("Entity set 'EMSContext.Events'  is null.");
            }

            var @event = _mapper.Map<Event>(model);

            try
            {
                _context.Events.Add(@event);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            EventResponseModel response = _mapper.Map<EventResponseModel>(@event);

            var createdEvent = _context.Events.FirstOrDefault(x => x.Title == @event.Title);

            int eventId = 0;

            if (createdEvent != null)
            {
                eventId = createdEvent.EventId;
            }

            return new Dictionary<int, EventResponseModel> {
                { eventId, response }
            };
        }

        public void Delete(int id)
        {
            if (_context.Events == null)
            {
                throw new Exception("Not found");
            }
            var @event = _context.Events.Find(id);

            if (@event == null)
            {
                throw new Exception("Not found");
            }

            try
            {
                _context.Events.Remove(@event);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private bool EventExists(int id)
        {
            return (_context.Events?.Any(e => e.EventId == id)).GetValueOrDefault();
        }
    }
}