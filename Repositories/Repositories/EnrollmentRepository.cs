using AutoMapper;
using ems_backend.Data;
using ems_backend.Entities;
using ems_backend.Models;

namespace ems_backend.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly EMSContext _context;
        private readonly IMapper _mapper;

        public EnrollmentRepository(EMSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<EnrollmentResponseModel> GetAll()
        {
            if (_context.Enrollments == null)
            {
                throw new Exception("Enrollments is null!");
            }

            List<EnrollmentResponseModel> enrollmentList = new List<EnrollmentResponseModel>();

            foreach (var enrollment in _context.Enrollments.ToList())
            {
                enrollmentList.Add(_mapper.Map<EnrollmentResponseModel>(enrollment));
            }

            return enrollmentList;
        }
        public EnrollmentResponseModel GetById(int id)
        {
            if (_context.Enrollments == null)
            {
                throw new Exception("Enrollment not found!");
            }
            var enrollment = _context.Enrollments.Find(id);

            if (enrollment == null)
            {
                throw new Exception("Enrollment is null");
            }

            var mappedEnrollment = _mapper.Map<EnrollmentResponseModel>(enrollment);

            return mappedEnrollment;
        }
        public Dictionary<int, EnrollmentResponseModel> Create(EnrollmentRequestModel model)
        {
            if (_context.Enrollments == null)
            {
                throw new Exception("Entity set 'EMSContext.Enrollments'  is null.");
            }

            Enrollment enrollment = _mapper.Map<Enrollment>(model);

            try
            {
                _context.Enrollments.Add(enrollment);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            EnrollmentResponseModel response = _mapper.Map<EnrollmentResponseModel>(enrollment);

            return new Dictionary<int, EnrollmentResponseModel> {
                { enrollment.EnrollmentId, response }
            };
        }

        public IEnumerable<Event> GetEventsByUserID(int id) {
            if (_context.Users.FirstOrDefault(x => x.UserId == id) == null)
            {
                throw new Exception("User not found!");
            }

            List<Enrollment> userEnrollments = _context.Enrollments.Where(x => x.UserId == id).ToList();
            List<Event> events = new List<Event>();

            foreach (var enrollment in userEnrollments)
            {
                var eventId = enrollment.EventId;
                var @event = _context.Events.FirstOrDefault(x => x.EventId == eventId);

                if (@event != null)
                    events.Add(@event);
            }

            return events;
        }

        public void Delete(int id)
        {
            if (_context.Enrollments == null)
            {
                throw new Exception("Enrollments is null");
            }

            var enrollment = _context.Enrollments.Find(id);
            
            if (enrollment == null)
            {
                throw new Exception("Not found!");
            }

            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();
        }

        public bool isEnrolled(int eventID, int userID)
        {
            var enrollment = _context.Enrollments.FirstOrDefault(x =>
                x.EventId == eventID &&
                x.UserId == userID
            );

            return enrollment != null;
        }
    }
}