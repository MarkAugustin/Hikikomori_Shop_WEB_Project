using Microsoft.AspNetCore.Mvc;

namespace HikikomoriShop.UI.Controllers;

public class CategoryController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}