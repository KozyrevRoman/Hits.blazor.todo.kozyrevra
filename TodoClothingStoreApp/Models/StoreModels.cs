using System.ComponentModel.DataAnnotations;

namespace TodoClothingStoreApp.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите название товара")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Название должно быть от 3 до 50 символов")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите размер")]
    public string Size { get; set; } = "M";

    [Range(100, 100000, ErrorMessage = "Цена должна быть от 100 до 100 000 ₽")]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Выберите категорию")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}

public class CartItem
{
    public Product Product { get; set; } = new();
    public int Quantity { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class CheckoutFormModel
{
    [Required(ErrorMessage = "Укажите ваше имя для заказа")]
    [StringLength(40, MinimumLength = 2, ErrorMessage = "Имя должно содержать минимум 2 буквы")]
    public string CustomerName { get; set; } = string.Empty;
}
