
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSphere.Data;

namespace ShopSphere.Controllers;

public class StoreController : Controller
{
    private readonly AppDbContext _context;

    public StoreController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? search, int? categoryId)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p =>
                p.Name.Contains(search) ||
                p.Description.Contains(search));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Search = search;
        ViewBag.CategoryId = categoryId;

        var products = await query
            .OrderBy(p => p.Name)
            .ToListAsync();

        return View(products);
    }
}
