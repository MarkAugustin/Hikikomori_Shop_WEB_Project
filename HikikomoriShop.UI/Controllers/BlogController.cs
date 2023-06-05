using Microsoft.AspNetCore.Mvc;

namespace HikikomoriShop.UI.Controllers;

public class BlogController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}