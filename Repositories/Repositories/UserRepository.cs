using AutoMapper;
using ems_backend.Data;
using ems_backend.Entities;
using ems_backend.Models;

namespace ems_backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EMSContext _context;
        private readonly IMapper _mapper;

        public UserRepository(EMSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<UserResponseModel> GetAll()
        {
            if (_context.Users == null)
            {
                throw new Exception("Not found");
            }

            var userList = new List<UserResponseModel>();


            foreach (var user in _context.Users.ToList())
            {
                userList.Add(_mapper.Map<UserResponseModel>(user));
            }

            return userList;
        }

        public UserResponseModel GetById(int id)
        {
            if (_context.Users == null)
            {
                throw new Exception("Not found");
            }
            var user = _context.Users.Find(id);

            if (user == null)
            {
                throw new Exception("Not found");
            }

            var mappedUser = _mapper.Map<UserResponseModel>(user);

            return mappedUser;
        }

        public void Update(int id, UserRequestModel model)
        {
            var user = _mapper.Map<User>(model);
            user.UserId = id;

            _context.Users.Update(user);

            try
            {
                _context.SaveChanges();
            }
            catch
            {
                if (!UserExists(id))
                {
                    throw new Exception("Not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public Dictionary<int, UserResponseModel> Create(UserRequestModel model)
        {
            if (_context.Users == null)
            {
                throw new Exception("Entity set 'EMSContext.Users'  is null.");
            }

            User user = _mapper.Map<User>(model);

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            UserResponseModel response = _mapper.Map<UserResponseModel>(user);

            var createdUser = _context.Users.FirstOrDefault(x => x.Username == user.Username);

            int userId = 0;

            if (createdUser != null)
            {
                userId = createdUser.UserId;
            }

            return new Dictionary<int, UserResponseModel> {
                { userId, response}
            };
        }
        public void Delete(int id)
        {
            if (_context.Users == null)
            {
                throw new Exception("Not found");
            }

            var user = _context.Users.Find(id);

            if (user == null)
            {
                throw new Exception("Not found");
            }

            try
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}