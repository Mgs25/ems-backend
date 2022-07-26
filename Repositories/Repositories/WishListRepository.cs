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

        public IEnumerable<Event> GetEventsByUserID(int id)
        {
            if (_context.Users.FirstOrDefault(x => x.UserId == id) == null)
            {
                throw new Exception("User not found!");
            }

            List<WishList> userWishList = _context.WishList.Where(x => x.UserId == id).ToList();
            List<Event> events = new List<Event>();

            foreach (var wish in userWishList)
            {
                var eventId = wish.EventId;
                var @event = _context.Events.FirstOrDefault(x => x.EventId == eventId);

                if (@event != null)
                    events.Add(@event);
            }

            return events;
        }

        public bool EventExists(WishListRequestModel model)
        {
            bool exists;
            try
            {
                exists = _context.WishList.FirstOrDefault(x => 
                    x.EventId == model.EventID &&
                    x.UserId == model.UserID
                ) != null ? true : false;
                return exists;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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

        public void Delete(WishListRequestModel model)
        {
            try
            {
                var wish = _context.WishList.FirstOrDefault(x => x.EventId == model.EventID && x.UserId == model.UserID);
                if (wish != null)
                    _context.WishList.Remove(wish);
                else
                    throw new Exception("Not found");
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}