using e_commerce.Models.Product;
using e_commerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController :  ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [Authorize]
    [HttpGet("get-all-products")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProducts();
        
        return Ok(products);
    }

    [Authorize]
    [HttpPost("add-product")]
    public async Task<IActionResult> AddProduct([FromBody] ProductAddDTO dto)
    {
        var product = await _productService.AddProduct(dto);
        
        if (product == null)
            return BadRequest("Product already exists");
        
        return Ok(product);
    }
    
}