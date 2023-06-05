using Microsoft.AspNetCore.Mvc;

namespace HikikomoriShop.UI.Controllers;

public class CartController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}