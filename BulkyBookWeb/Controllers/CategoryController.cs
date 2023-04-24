using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace BulkyBookWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET
    public IActionResult Index()
    {
        IEnumerable<Category> categories = _db.Categories;
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (!ModelState.IsValid) return View();
        if (category.Name.Equals(category.DisplayOrder.ToString()))
        {
            ModelState.AddModelError("CustomError", "The display count can not be same as the name");
        }

        _db.Categories.Add(category);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        var category = _db.Categories.Find(id);
        // var categorySingle = _db.Categories.SingleOrDefault(cat => cat.Id == id);
        // var categoryFirst = _db.Categories.FirstOrDefault(cat => cat.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid) return View();
        if (category.Name.Equals(category.DisplayOrder.ToString()))
        {
            ModelState.AddModelError("CustomError", "The display count can not be same as the name");
        }

        _db.Categories.Update(category);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}