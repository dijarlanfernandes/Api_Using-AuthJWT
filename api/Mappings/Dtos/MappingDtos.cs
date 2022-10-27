using api.Controllers;
using api.Models;
using Api.Models.Dtos;

namespace api.Mappings.Dtos
{
    public static class MappingDtos
    {
        public static ProductDto ConvertProductToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                name = product.name,
                description = product.description,
                price = product.price,
                quantity = product.quantity,
                categoryid = product.categoryid
               
            };                           
        }
        public static IEnumerable<ProductDto> ConvertListProductToDtos( this IEnumerable<Product> products)
        {
           return products.Select(x => new ProductDto
           {
               Id = x.Id,
               name = x.name,
               description = x.description,
               price = x.price,
               quantity = x.quantity,
               categoryid = x.category.categoryid,
               categoryname = x.category.name
           }).OrderBy(x=>x.name).ToList();
        }
        public static IEnumerable<CategoryDto> ConvertListCategoriesToDtos(this IEnumerable<Category> categories)
        {
            return categories.Select(x => new CategoryDto
            {
                categoryid = x.categoryid,
                name = x.name                
            }).OrderBy(x=>x.name).ToList();
        }
        public static CategoryDto ConvertCategoryToDto(Category category)
        {
            return new CategoryDto
            {
                categoryid = category.categoryid,
                name = category.name                
            };
        }
        public static UserRegistrationRequestDto ConvertToUserRegistrationToDto( this User user) 
        {
            return new UserRegistrationRequestDto
            {
                name = user.name,
                email = user.email,
                password  = user.password
            };
        } 
        public static IEnumerable<UserRegistrationRequestDto> ConvertListUserRegistrationToDtos(this IEnumerable<User> users)
        {
            return (users.Select(x => new UserRegistrationRequestDto
            {
                name = x.name,
                email = x.email,
                password = x.password
            }));
        }
        
    }
}
