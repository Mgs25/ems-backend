using ems_backend.Models;

namespace ems_backend.Repositories
{
    public interface IWishListRepository
    {
        IEnumerable<WishListResponseModel> GetAll();
        WishListResponseModel GetById(int id);
        Dictionary<int, WishListResponseModel> Create(WishListRequestModel model);
        void Delete(int id);
    }
}