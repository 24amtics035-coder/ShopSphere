
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopSphere.Models;

public class Product
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 10000000,
        ErrorMessage = "Price must be greater than zero")]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Range(0, 100000)]
    public int Stock { get; set; }

    [StringLength(500)]
    public string? ImageUrl { get; set; }

    [Required(ErrorMessage = "Please select a category")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}

