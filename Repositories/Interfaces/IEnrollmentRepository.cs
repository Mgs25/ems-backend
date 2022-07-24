using ems_backend.Entities;
using ems_backend.Models;

namespace ems_backend.Repositories
{
    public interface IEnrollmentRepository
    {
        IEnumerable<EnrollmentResponseModel> GetAll();
        EnrollmentResponseModel GetById(int id);
        IEnumerable<Event> GetEventsByUserID(int id);
        Dictionary<int, EnrollmentResponseModel> Create(EnrollmentRequestModel model);
        void Delete(int id);
    }
}