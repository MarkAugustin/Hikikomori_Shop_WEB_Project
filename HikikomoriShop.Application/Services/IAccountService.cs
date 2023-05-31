using HikikomoriShop.Domain;

namespace HikikomoriShop.Application.Services;

public interface IAccountService
{
    Task SaveAsync();
    void InsertAccount (User user);
    void InsertRole (Role role);
    Task<Role> GetRoleByNameAsync(string name);
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByIdAsync(string id);
    Task<User> GetByIdWithoutTrackingAsync(string id);
    Task<User> LoginAsync(string email, string password);
    Task<List<User>> GetAllAsync();
    void Update(User user);
    void Delete(User user);
}