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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Haggis",
                Description = "Tipycal Scottish meal. From Highlands",
                CategoryName = "Meal",
                Price = 7.99,
                ImageUrl = "https://microserviceproject.blob.core.windows.net/scot/haggis.jpg"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Mac & Cheese",
                Description = "Another typical scottish meal. From... well, could be anywhere :D",
                CategoryName = "Meal",
                Price = 14.75,
                ImageUrl = "https://microserviceproject.blob.core.windows.net/scot/mac-and-cheese.png"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Pizza with Haggis",
                Description = "Pizza with cheese, tomato, basil and haggis. Yum yum!",
                CategoryName = "Meal",
                Price = 10.25,
                ImageUrl = "https://microserviceproject.blob.core.windows.net/scot/pizza_haggis.png"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Royal Hamburger",
                Description = "Directly from her Majesty The Queen, the biggest hamburger! Only for faggots",
                CategoryName = "Meal",
                Price = 16.89,
                ImageUrl = "https://microserviceproject.blob.core.windows.net/scot/royal_hamburger.jpg"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 5,
                Name = "French fries",
                Description = "French. Fries.",
                CategoryName = "Side",
                Price = 6.55,
                ImageUrl = "https://microserviceproject.blob.core.windows.net/scot/French_Fries.jpg"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 6,
                Name = "Snaks",
                Description = "Mixture of Snacks",
                CategoryName = "Side",
                Price = 3.95,
                ImageUrl = "https://microserviceproject.blob.core.windows.net/scot/snacks.jpg"
            });
        }
    }
}
