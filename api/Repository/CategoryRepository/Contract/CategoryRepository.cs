using api.Domain.Data;
using api.Models;
using api.Repository.CategoryRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.CategoryRepository.Contract
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;        

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
            
        }       

        public async Task<Category> PostCategory(Category category)
        {
            try
            {
                var categories = await _context.categories.AddAsync(category);
                if (categories.Entity.name != null)
                {
                    string.Join("",$"ja existe{categories.Entity.name}").ToString();
                }
                await _context.SaveChangesAsync();
                return categories.Entity;

            }
            catch (Exception)
            {

                throw;
            }            
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var categories =await _context.categories.OrderBy(category => category.name).ToListAsync();
            return categories;
        }
    }
}
