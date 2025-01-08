namespace OnlineShopping.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }

    public ICollection<Order> Orders { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; } // soft delete
}