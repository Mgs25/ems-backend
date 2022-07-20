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
    }
}