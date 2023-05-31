using Microsoft.EntityFrameworkCore;
using HikikomoriShop.Domain.Entities;
using HikikomoriShop.Infrastructure;

namespace HikikomoriShop.Application.Services;

public interface IProductService
{
    Task SaveAsync();
    void InsertProduct(Product product);
    void Update(Product product);
    void Delete(Product product);
    Task<Product> GetByIdAsync(string id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetAllWithCategoryAsync();
    Task<IEnumerable<Product>> GetByCategoryAsync(string categoryName);
    Task<Product> GetByNameAsync(string name);
    Task<bool> IsExistByIdAsync(string id);
    Task<bool> IsExistByNameAsync(string name);
    void InsertCategory(Category category);
    Task<Category> GetCategoryByNameAsync(string name);
}

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    
    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public Task SaveAsync()
    {
        return _context.SaveChangesAsync();
    }
    
    public void InsertProduct(Product product)
    {
        _context.Products.Add(product);
    }
    
    public void Update(Product product)
    {
        _context.Products.Update(product);
    }
    
    public void Delete(Product product)
    {
        _context.Products.Remove(product);
    }
    
    public async Task<Product> GetByIdAsync(string id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p != null && p.Id == id);
    }
    
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }
    
    public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }
    
    public async Task<IEnumerable<Product>> GetByCategoryAsync(string categoryName)
    {
        return await _context.Products.Where(p => p.Category.Name == categoryName).ToListAsync();
    }
    
    public async Task<Product> GetByNameAsync(string name)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<bool> IsExistByIdAsync(string id)
    {
        return await _context.Products.AnyAsync(p => p != null && p.Id == id);
    }
    
    public async Task<bool> IsExistByNameAsync(string name)
    {
        return await _context.Products.AnyAsync(p => p != null && p.Name == name);
    }
    
    public void InsertCategory(Category category)
    {
        _context.Categories.Add(category);
    }
    
    public async Task<Category> GetCategoryByNameAsync(string name)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c != null && c.Name == name);
    }
}