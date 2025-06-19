using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Models.Product;

public class Product
{
    [Column("idProduct")]
    public int Id { get; set; }

    public required string Name { get; set; } = null!;
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public int? CategoryId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}