using Microsoft.AspNetCore.Mvc;
using ems_backend.Data;
using ems_backend.Models;
using AutoMapper;
using ems_backend.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace ems_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class CategoryController : ControllerBase
    {
        private readonly EMSContext _context;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(EMSContext context, IMapper mapper, ICategoryRepository categoryRepo)
        {
            _context = context;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryResponseModel>> GetCategories()
        {
            try
            {
                var categoriesList = _categoryRepo.GetAll();
                return Ok(categoriesList);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryResponseModel> GetCategory(int id)
        {
            try
            {
                var category = _categoryRepo.GetById(id);
                return Ok(category);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<CategoryResponseModel> PostCategory(CategoryRequestModel model)
        {
            try
            {
                Dictionary<int, CategoryResponseModel> response = _categoryRepo.Create(model);
                int categoryId = response.Keys.First();
                return CreatedAtAction("GetCategory", new { id = categoryId }, response[categoryId]);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _categoryRepo.Delete(id);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
