using ems_backend.Models;

namespace ems_backend.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<EventResponseModel> GetAll();
        EventResponseModel GetById(int id);
        void Update(int id, EventRequestModel model);
        Dictionary<int, EventResponseModel> Create(EventRequestModel model);
        void Delete(int id);
        
    }
}