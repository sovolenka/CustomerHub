using Data.Context;
using Data.Models;

namespace Business.Services;

public class ProductService
{
    private readonly SQLiteContext _context = SQLiteContextSingleton.Instance;

    public Product? Add(Product product, User user)
    {
        product.User = user;
        Product? added = _context.Products?.Add(product).Entity;
        return added;
    }

    public Product? Remove(Product product)
    {
        Product? removed = _context.Products?.Remove(product).Entity;
        _context.SaveChanges();
        return removed;
    }

    public Product? Update(Product product)
    {
        Product? updated = _context.Products?.Update(product).Entity;
        _context.SaveChanges();
        return updated;
    }
}