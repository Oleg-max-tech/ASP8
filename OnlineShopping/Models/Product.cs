using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Count { get; set; }

    public double OriginalPrice { get; set; }
    
    [Range(0.0, 1.0)]
    public double Discount { get; set; }

    public double Price => OriginalPrice * Discount;

    public ICollection<Order> Orders { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; } // soft delete

    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}