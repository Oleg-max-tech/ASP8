public interface IProductService
{
    IEnumerable<Product> GetAllProducts(bool includeDeleted = false);
    Product GetProductById(int id);
    Product AddProduct(Product product);
    bool DeleteProduct(int id);
    IEnumerable<Product> SearchProductsByName(string name);
    IEnumerable<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice);
}
