using TodoClothingStoreApp.Data;
using TodoClothingStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoClothingStoreApp.Services;

public class StoreService
{
    private readonly ApplicationDbContext _db;

    public StoreService(ApplicationDbContext db)
    {
        _db = db;
    }

    public List<CartItem> Cart { get; } = new();

    public async Task<List<Category>> GetCategoriesAsync() =>
        await _db.Categories.ToListAsync();

    public async Task<List<Product>> GetProductsAsync() =>
        await _db.Products.Include(p => p.Category).ToListAsync();

    public async Task<List<Order>> GetOrdersAsync() =>
        await _db.Orders.Include(o => o.Items).OrderByDescending(o => o.OrderDate).ToListAsync();

    public void AddToCart(Product product)
    {
        var existing = Cart.FirstOrDefault(c => c.Product.Id == product.Id);
        if (existing != null)
            existing.Quantity++;
        else
            Cart.Add(new CartItem { Product = product, Quantity = 1 });
    }

    public void RemoveFromCart(int productId)
    {
        var item = Cart.FirstOrDefault(c => c.Product.Id == productId);
        if (item != null) Cart.Remove(item);
    }

    public async Task CheckoutAsync(string customerName)
    {
        if (!Cart.Any() || string.IsNullOrWhiteSpace(customerName)) return;

        var order = new Order
        {
            OrderDate = DateTime.Now,
            CustomerName = customerName,
            TotalAmount = Cart.Sum(c => c.Product.Price * c.Quantity),
            Items = Cart.Select(c => new OrderItem
            {
                ProductName = c.Product.Name,
                Size = c.Product.Size,
                Price = c.Product.Price,
                Quantity = c.Quantity
            }).ToList()
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        Cart.Clear();
    }

    public async Task AddProductAsync(Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product != null)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }
    }
}
