using ems_backend.Models;

namespace ems_backend.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryResponseModel> GetAll();
        CategoryResponseModel GetById(int id);
        Dictionary<int, CategoryResponseModel> Create(CategoryRequestModel model);
        void Delete(int id);
    }
}