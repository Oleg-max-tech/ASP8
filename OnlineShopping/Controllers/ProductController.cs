using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Services;
using OnlineShopping.Models;

[Route("api/shop/products")]
[ApiController]

public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = _productService.GetAllProducts(includeDeleted: false);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = _productService.GetProductById(id);
        if (product == null || product.DeletedAt != null)
        {
            return NotFound("Product not found or deleted.");
        }
        return Ok(product);
    }

    [HttpPost]
    public IActionResult AddProduct([FromBody] Product product)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var newProduct = _productService.AddProduct(product);
        return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var result = _productService.DeleteProduct(id);
        if (!result) return NotFound("Product not found.");
        return NoContent();
    }

    [HttpGet("search")]
    public IActionResult SearchProducts([FromQuery] string name)
    {
        var products = _productService.SearchProductsByName(name);
        return Ok(products);
    }

    [HttpGet("range")]
    public IActionResult GetProductsByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
    {
        var products = _productService.GetProductsByPriceRange(minPrice, maxPrice);
        return Ok(products);
    }
}