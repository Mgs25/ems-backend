using AutoMapper;
using ems_backend.Data;
using ems_backend.Entities;
using ems_backend.Models;

namespace ems_backend.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EMSContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(EMSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<CategoryResponseModel> GetAll()
        {
            if (_context.Categories == null)
            {
                throw new Exception("Not found");
            }

            var categorieslist = new List<CategoryResponseModel>();

            foreach (var category in _context.Categories.ToList())
            {
                categorieslist.Add(_mapper.Map<CategoryResponseModel>(category));
            }

            return categorieslist;
        }
        public CategoryResponseModel GetById(int id)
        {
            if (_context.Categories == null)
            {
                throw new Exception("Not found");
            }
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                throw new Exception("Not found");
            }

            var mappedCategory = _mapper.Map<CategoryResponseModel>(category);

            return mappedCategory;
        }
        public Dictionary<int, CategoryResponseModel> Create(CategoryRequestModel model)
        {
            if (_context.Categories == null)
            {
                throw new Exception("Entity set 'EMSContext.Users'  is null.");
            }

            Category category = _mapper.Map<Category>(model);

            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            CategoryResponseModel response = _mapper.Map<CategoryResponseModel>(category);

            var createdCategory = _context.Categories.FirstOrDefault(x => x.Title == model.Title);

            int categoryId = 0;

            if (createdCategory != null)
            {
                categoryId = createdCategory.CategoryId;
            }

            return new Dictionary<int, CategoryResponseModel> {
                { categoryId, response }
            };
        }
        public void Delete(int id)
        {
            if (_context.Categories == null)
            {
                throw new Exception("Not found");
            }

            var category = _context.Categories.Find(id);

            if (category == null)
            {
                throw new Exception("Not found");
            }

            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}