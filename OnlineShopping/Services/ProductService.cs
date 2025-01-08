using System;

public class Class1
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAllProducts(bool includeDeleted = false)
    {
        return includeDeleted
            ? _context.Products.ToList()
            : _context.Products.Where(p => p.DeletedAt == null).ToList();
    }

    public Product GetProductById(int id)
    {
        return _context.Products.SingleOrDefault(p => p.Id == id);
    }

    public Product AddProduct(Product product)
    {
        product.CreatedAt = DateTime.UtcNow;
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public bool DeleteProduct(int id)
    {
        var product = _context.Products.SingleOrDefault(p => p.Id == id);
        if (product == null) return false;

        product.DeletedAt = DateTime.UtcNow; // Soft delete
        _context.SaveChanges();
        return true;
    }

    public IEnumerable<Product> SearchProductsByName(string name)
    {
        return _context.Products
            .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public IEnumerable<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
    {
        return _context.Products
            .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
            .OrderBy(p => p.Price)
            .ToList();
    }
}
