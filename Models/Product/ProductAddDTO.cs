using System.ComponentModel.DataAnnotations;

namespace e_commerce.Models.Product;

public class ProductAddDTO
{
    [Required] public string Name { get; set; } = null!;

    public string? Description { get; set; }
    
    [Required] public decimal Price { get; set; }
    
    public int CategoryId { get; set; }
}