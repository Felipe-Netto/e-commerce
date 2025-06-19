using e_commerce.Data;
using e_commerce.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Services;

public class ProductService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public ProductService(IConfiguration configuration, ApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<Product?> AddProduct(ProductAddDTO dto)
    {
        if (await _context.Products.AnyAsync(p => p.Name == dto.Name))
            return null;

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
        };
        
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }
    
    public async Task<List<Product>> GetAllProducts()
    {
        return await _context.Products.ToListAsync();
    }
    
}