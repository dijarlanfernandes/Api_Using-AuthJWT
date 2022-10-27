using api.Models;

namespace api.Repository.CategoryRepository.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> PostCategory(Category category);
        Task<IEnumerable<Category>> GetAllCategories();
    }
}
