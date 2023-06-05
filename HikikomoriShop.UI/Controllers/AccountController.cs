using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HikikomoriShop.Application.Services;
using HikikomoriShop.Domain;

namespace HikikomoriShop.UI.Controllers;

[Controller]
[Route("[controller]/[action]")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var users = await _accountService.GetAllAsync();
        return View(users);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
public async Task<IActionResult> Delete(string id)
    {
        var user = await _accountService.GetByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        return View(user);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var user = await _accountService.GetByIdAsync(id);
        _accountService.Delete(user);
        await _accountService.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Create(User user)
    {
        return View(user);
    }
    
    [Authorize(Roles = "Admin")]
    [ActionName("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateConfirmed(User user)
    {
        var userFromDb = await _accountService.GetByEmailAsync(user.Email);
        if (userFromDb is not null)
        {
            return RedirectToAction("Login", "AccountSession");
        }
        
        var roleFromDb = await _accountService.GetRoleByNameAsync(user.Role?.Name);
        if (roleFromDb is null)
        {
            roleFromDb = new Role
            {
                Id = Guid.NewGuid().ToString(),
                Name = user.Role?.Name
            };
            _accountService.InsertRole(roleFromDb);
        }
            
        userFromDb = new User
        {
            Id = Guid.NewGuid().ToString(), 
            Email = user.Email, 
            Password = user.Password,
            Role = roleFromDb
        };
            
        _accountService.InsertAccount(userFromDb);   
        await _accountService.SaveAsync();
        return RedirectToAction(nameof(Index));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Update(string id)
    {
        var user = await _accountService.GetByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        return View(user);
    }
    
    [ActionName("Update")]
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateConfirmed(User user)
    {
        var roleFromDb = await _accountService.GetRoleByNameAsync(user.Role?.Name);
        if (roleFromDb is not null)
        {
            user.Role = roleFromDb; 
        }
        else
        {
            user.Role = await _accountService.GetRoleByNameAsync("User");
        }
        
        _accountService.Update(user);
        await _accountService.SaveAsync();
        return RedirectToAction(nameof(Index));
    }
}