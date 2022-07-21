using AutoMapper;
using ems_backend.Data;
using ems_backend.Entities;
using ems_backend.Models;

namespace ems_backend.Repositories
{
    public class WishListRepository : IWishListRepository
    {
        private readonly EMSContext _context;
        private readonly IMapper _mapper;

        public WishListRepository(EMSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<WishListResponseModel> GetAll()
        {
            if (_context.WishList == null)
            {
                throw new Exception("Not found");
            }

            var wishLists = new List<WishListResponseModel>();


            foreach (var wish in _context.WishList.ToList())
            {
                wishLists.Add(_mapper.Map<WishListResponseModel>(wish));
            }

            return wishLists;
        }

        public WishListResponseModel GetById(int id)
        {
            if (_context.WishList == null)
            {
                throw new Exception("Not found");
            }
            var wish = _context.WishList.Find(id);

            if (wish == null)
            {
                throw new Exception("Not found");
            }

            var mappedWish = _mapper.Map<WishListResponseModel>(wish);

            return mappedWish;
        }

        public Dictionary<int, WishListResponseModel> Create(WishListRequestModel model)
        {
            if (_context.WishList == null)
            {
                throw new Exception("Entity set 'EMSContext.Users'  is null.");
            }

            WishList wish = _mapper.Map<WishList>(model);

            try
            {
                _context.WishList.Add(wish);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            WishListResponseModel response = _mapper.Map<WishListResponseModel>(wish);

            var createdWish = _context.WishList.FirstOrDefault(x => x.EventId == model.EventID);

            int wishId = 0;

            if (createdWish != null)
            {
                wishId = createdWish.UserId;
            }

            return new Dictionary<int, WishListResponseModel> {
                { wishId, response}
            };
        }

        public void Delete(int id)
        {
            if (_context.WishList == null)
            {
                throw new Exception("Not found");
            }

            var wish = _context.WishList.Find(id);

            if (wish == null)
            {
                throw new Exception("Not found");
            }

            try
            {
                _context.WishList.Remove(wish);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}