using api.Models;

namespace api.Repository.ProductRepository.Interface
{
    public interface IProduct
    {
        Task<IEnumerable<Product>> GetProductsAllAsync();
        Task<Product> GetProductsByIdAsync(int id);
        Task<Product> GetProductByNameAsync(string name);
        Task<Product> GetCategoriesByIdAsync(int id);
        Task<Product> PostProduct(Product product);
    }
}
