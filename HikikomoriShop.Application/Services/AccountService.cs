using Microsoft.EntityFrameworkCore;
using HikikomoriShop.Domain;
using HikikomoriShop.Infrastructure;

namespace HikikomoriShop.Application.Services;

public class AccountService : IAccountService
{
    private readonly ApplicationDbContext _context;
    
    public AccountService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void InsertAccount(User user)
    {
        _context.Users.Add(user);
    }
    
    public void InsertRole(Role role)
    {
        _context.Roles.Add(role);
    }

    public void Update(User user)
    {
        user.Role = user.Role;
        _context.Users.Update(user);
    }

    public void Delete(User user)
    {
        _context.Users.Remove(user);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u != null && u.Email == email);
    }
    
    public async Task<User> GetByIdAsync(string id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u != null && u.Id == id);
    }
    
    public async Task<User> GetByIdWithoutTrackingAsync(string id)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u != null && u.Id == id);
    }
    
    public async Task<Role> GetRoleByNameAsync(string name)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r != null && r.Name == name);
    }
    
    public async Task<User> LoginAsync(string email, string password)
    {
        return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u 
            => u != null && u.Email == email && u.Password == password);
    }
    
    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.Include(u => u.Role).ToListAsync();
    }
}