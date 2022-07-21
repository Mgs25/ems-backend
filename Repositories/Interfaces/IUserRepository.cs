using ems_backend.Models;

namespace ems_backend.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserResponseModel> GetAll();
        UserResponseModel GetById(int id);
        void Update(int id, UserRequestModel model);
        Dictionary<int, UserResponseModel> Create(UserRequestModel model);
        void Delete(int id);
    }
}