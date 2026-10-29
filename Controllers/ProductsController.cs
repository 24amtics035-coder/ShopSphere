
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopSphere.Data;
using ShopSphere.Models;

namespace ShopSphere.Controllers;

public class ProductsController : Controller
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .ToListAsync();

        return View(products);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(
            _context.Categories, "CategoryId", "Name");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (!await _context.Categories
                .AnyAsync(c => c.CategoryId == product.CategoryId))
        {
            ModelState.AddModelError(
                nameof(product.CategoryId), "Select a valid category.");
        }

        if (ModelState.IsValid)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["CategoryId"] = new SelectList(
            _context.Categories, "CategoryId", "Name",
            product.CategoryId);

        return View(product);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        ViewData["CategoryId"] = new SelectList(
            _context.Categories, "CategoryId", "Name",
            product.CategoryId);

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id, Product product)
    {
        if (id != product.ProductId)
            return NotFound();

        if (!await _context.Categories
                .AnyAsync(c => c.CategoryId == product.CategoryId))
        {
            ModelState.AddModelError(
                nameof(product.CategoryId), "Select a valid category.");
        }

        if (ModelState.IsValid)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["CategoryId"] = new SelectList(
            _context.Categories, "CategoryId", "Name",
            product.CategoryId);

        return View(product);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
