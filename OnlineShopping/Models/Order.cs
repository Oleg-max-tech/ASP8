namespace OnlineShopping.Models;

public class Order
{
    public int Id { get; set; }
    public User User { get; set; }
    public Product Product { get; set; }

    public double PriceForDelivery { get; set; }
    public double TotalPrice => PriceForDelivery + Product.Price;

    public string Address { get; set; }
    
    public DateTime CreatedAt { get; set; }
}