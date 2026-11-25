using TodoClothingStoreApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoClothingStoreApp.Models;

namespace TodoClothingStoreApp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Верхняя одежда" },
            new Category { Id = 2, Name = "Футболки и лонгсливы" },
            new Category { Id = 3, Name = "Джинсы и брюки" }
        );

        builder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Куртка демисезонная", Size = "L", Price = 5500, CategoryId = 1 },
            new Product { Id = 2, Name = "Пальто шерстяное", Size = "M", Price = 8900, CategoryId = 1 },
            new Product { Id = 3, Name = "Футболка базовая белая", Size = "M", Price = 1200, CategoryId = 2 },
            new Product { Id = 4, Name = "Худи оверсайз черное", Size = "XL", Price = 3400, CategoryId = 2 },
            new Product { Id = 5, Name = "Джинсы прямого кроя", Size = "32", Price = 4200, CategoryId = 3 }
        );
    }
}
