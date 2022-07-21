using Microsoft.AspNetCore.Mvc;
using ems_backend.Data;
using ems_backend.Models;
using AutoMapper;
using ems_backend.Repositories;

namespace ems_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly EMSContext _context;
        private readonly IMapper _mapper;
        private readonly IWishListRepository _wishRepo;

        public WishListController(EMSContext context, IMapper mapper, IWishListRepository wishRepo)
        {
            _context = context;
            _mapper = mapper;
            _wishRepo = wishRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WishListResponseModel>> GetWishs()
        {
            try
            {
                var wishList = _wishRepo.GetAll();
                return Ok(wishList);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet("{id}")]
        public ActionResult<WishListResponseModel> GetWish(int id)
        {
            try
            {
                var category = _wishRepo.GetById(id);
                return Ok(category);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<WishListResponseModel> PostWish(WishListRequestModel model)
        {
            try
            {
                Dictionary<int, WishListResponseModel> response = _wishRepo.Create(model);
                int categoryId = response.Keys.First();
                return CreatedAtAction("GetWish", new { id = categoryId }, response[categoryId]);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWish(int id)
        {
            try
            {
                _wishRepo.Delete(id);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private bool WishExists(int id)
        {
            return (_context.WishList?.Any(e => e.WishListId == id)).GetValueOrDefault();
        }
    }
}
