using api.Models;
using api.Repository.CategoryRepository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]        
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            var list = await _categoryRepository.GetAllCategories();
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            var cat = await _categoryRepository.PostCategory(category);
            
            return Ok(cat);
        }
    }
}
