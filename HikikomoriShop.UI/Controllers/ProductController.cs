using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HikikomoriShop.Application.Services;
using HikikomoriShop.Domain.Entities;

namespace HikikomoriShop.UI.Controllers;

[Controller]
[Route("[controller]/[action]")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllWithCategoryAsync();
        return View(products);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
public async Task<IActionResult> Delete(string id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var productFromDb = await _productService.GetByIdAsync(id);
        _productService.Delete(productFromDb);
        await _productService.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Create(Product product)
    {
        return View(product);
    }
    
    [Authorize(Roles = "Admin")]
    [ActionName("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateConfirmed(Product product)
    {
        product.Id = Guid.NewGuid().ToString();
        product.Category = await _productService.GetCategoryByNameAsync("Default");
        _productService.InsertProduct(product);   
        await _productService.SaveAsync();
        return RedirectToAction(nameof(Index));
    }
}