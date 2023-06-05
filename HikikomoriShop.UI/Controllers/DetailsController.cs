using Microsoft.AspNetCore.Mvc;

namespace HikikomoriShop.UI.Controllers;

public class DetailsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}