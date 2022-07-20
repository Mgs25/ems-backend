using ems_backend.Models;

namespace ems_backend.Repositories
{
    public interface IEnrollmentRepository
    {
        IEnumerable<EnrollmentResponseModel> GetAll();
        EnrollmentResponseModel GetById(int id);
        Dictionary<int, EnrollmentResponseModel> Create(EnrollmentRequestModel model);
        void Delete(int id);
    }
}