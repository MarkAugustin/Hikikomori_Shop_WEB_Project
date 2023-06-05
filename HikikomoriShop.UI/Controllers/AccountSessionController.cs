using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using HikikomoriShop.Application.Services;
using HikikomoriShop.Domain;
using HikikomoriShop.Infrastructure;

namespace HikikomoriShop.UI.Controllers;

[Controller]
[Route("[controller]/[action]")]
public class AccountSessionController : Controller
{
    private readonly IAccountService _accountService;

    public AccountSessionController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(Register model)
    {
        if (ModelState.IsValid)
        {
            var user = await _accountService.GetByEmailAsync(model.Email);
            
            if (user is not null)
            {
                return RedirectToAction("Login", "AccountSession");
            }
            
            var roleFromDb = await _accountService.GetRoleByNameAsync("User");

            user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = model.Email,
                Password = model.Password,
                Role = roleFromDb
            };

            _accountService.InsertAccount(user);  
            await _accountService.SaveAsync();
            await Authenticate(user);

            return RedirectToAction("Index", "Home");
        }
        
        ModelState.AddModelError("", "Incorrect login and/or password");
        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("Cookies");
        return RedirectToAction("Index", "Home");
    }
    
    [Authorize()]
    [HttpGet]
    public async Task<IActionResult> Logout(string id)
    {
        return View(id);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(Login model)
    {
        var user = await _accountService.LoginAsync(model.Email, model.Password);
        if (user is not null)
        {
            await Authenticate(user);
            return RedirectToAction("Index", "Home");
        }


        return View(model);
    }

    private async Task Authenticate(User? user)
    {
        if (user.Role?.Name != null)
        {
            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}