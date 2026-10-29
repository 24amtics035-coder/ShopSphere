
using System.ComponentModel.DataAnnotations;

namespace ShopSphere.Models;

public class Category
{
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Category name is required")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; }
        = new List<Product>();
}
