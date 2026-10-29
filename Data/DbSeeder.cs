
using Microsoft.EntityFrameworkCore;
using ShopSphere.Models;

namespace ShopSphere.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        string[] categoryNames =
        {
            "Shoes",
            "Clothing",
            "Electronics",
            "Accessories"
        };

        foreach (var name in categoryNames)
        {
            if (!await context.Categories.AnyAsync(c => c.Name == name))
            {
                context.Categories.Add(new Category
                {
                    Name = name
                });
            }
        }

        await context.SaveChangesAsync();

        // Avoid adding duplicate starter products.
        if (await context.Products.AnyAsync())
            return;

        var categories = await context.Categories
            .ToDictionaryAsync(c => c.Name, c => c.CategoryId);

        var products = new List<Product>
        {
            new()
            {
                Name = "Classic Red Sneakers",
                Description = "Sporty sneakers for everyday wear.",
                Price = 1499m,
                Stock = 20,
                CategoryId = categories["Shoes"],
                ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new()
            {
                Name = "Casual White Sneakers",
                Description = "Versatile footwear for casual outfits.",
                Price = 1799m,
                Stock = 15,
                CategoryId = categories["Shoes"],
                ImageUrl = "https://images.unsplash.com/photo-1495555961986-6d4c1ecb7be3?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new()
            {
                Name = "Classic Cotton T-Shirt",
                Description = "A comfortable everyday essential.",
                Price = 599m,
                Stock = 30,
                CategoryId = categories["Clothing"],
                ImageUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new()
            {
                Name = "Wireless Headphones",
                Description = "Enjoy music and everyday audio.",
                Price = 2499m,
                Stock = 12,
                CategoryId = categories["Electronics"],
                ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new()
            {
                Name = "Minimal Wristwatch",
                Description = "A timeless accessory for everyday outfits.",
                Price = 1999m,
                Stock = 10,
                CategoryId = categories["Accessories"],
                ImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new()
            {
                Name = "Everyday Backpack",
                Description = "A practical backpack for work and travel.",
                Price = 1299m,
                Stock = 18,
                CategoryId = categories["Accessories"],
                ImageUrl = "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new()
            {
                Name = "Classic Sunglasses",
                Description = "A versatile accessory for sunny days.",
                Price = 799m,
                Stock = 25,
                CategoryId = categories["Accessories"],
                ImageUrl = "https://images.unsplash.com/photo-1511499767150-a48a237f0083?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new()
            {
                Name = "Digital Camera",
                Description = "Capture your favourite moments.",
                Price = 8999m,
                Stock = 5,
                CategoryId = categories["Electronics"],
                ImageUrl = "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?auto=format&fit=crop&w=600&h=600&q=80"
            }
        };

        context.Products.AddRange(products);
        await context.SaveChangesAsync();
    }
}
