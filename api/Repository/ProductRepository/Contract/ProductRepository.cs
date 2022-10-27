using api.Domain.Data;
using api.Mappings.Dtos;
using api.Models;
using api.Repository.ProductRepository.Interface;
using Api.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace api.Repository.ProductRepository.Contract
{
    public class ProductRepository : IProduct
    {
        private readonly AppDbContext _context;
       

        public ProductRepository(AppDbContext context)
        {
            _context = context;            
        }

        public async Task<Product> GetCategoriesByIdAsync(int id)
        {
            try
            {
                var product = await _context.products
                    .Include(x => x.category).FirstOrDefaultAsync(
                                x => x.category.categoryid == id);

                if (product == null)
                {
                    return null;
                }
                return product;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            try
            {
             var products =await _context.products
                    .FirstOrDefaultAsync(x => x.name
                    .Contains(name, StringComparison.OrdinalIgnoreCase));

                if (products == null)
                {
                    return null;
                }
                return products;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsAllAsync()
        
        {
            try
            {
                var products = await _context.products.Include(p=>p.category).OrderBy(x => x.name).ToListAsync();
                if (products == null)
                {
                    return null;
                }                
                return products;
            }
            catch (Exception)
            {                
                throw;
            }
            
        }

        public Task<Product> GetProductsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        //private bool ValidateField(Product product)
        //{
        //   var name= _context.products.Find(product.name);           
        //    if (name == null)
        //    {
        //        String.Join("","not found");
        //    }            
        //    return false;
        //}
        public async Task<Product> PostProduct(Product product)
        {
            try
            {
                var prod = await _context.products.AddAsync(product);               
                await _context.SaveChangesAsync();
                return prod.Entity;
            }
            catch (Exception)
            {               
                throw;
            }
            
        }
    }
}
