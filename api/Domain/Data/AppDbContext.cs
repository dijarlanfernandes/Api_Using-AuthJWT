using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Domain.Data
{
    public class AppDbContext: IdentityDbContext<IdentityUser>
    {
        public DbSet<Product>products { get; set; }
        public DbSet<Category>categories { get; set; }        
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {}

    }
}
