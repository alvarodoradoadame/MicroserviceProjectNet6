using Microsoft.EntityFrameworkCore;
using Scot.Services.ProductAPI.Models;

namespace Scot.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        //Basic constructor for entityframework dbcontext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
