using Data.Context;
using Data.Models;

namespace Business.Services;

public class ProductService
{
    private readonly SQLiteContext _context = SQLiteContextSingleton.Instance;

    public Product? Add(Product product, User? user = null)
    {
        if (user is not null) product.User = user;
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

    public IEnumerable<Product> GetAll(User user)
    {
        return _context.Products!.Where(p => p.User == user);
    }

    public static bool ProductContains(Product product, string query)
    {
        query = query.ToLower();
        return product.Name!.ToLower().Contains(query) ||
            product.Price.ToString().Contains(query) ||
            product.Characteristic!.ProductType!.ToLower().Contains(query) ||
            product.Characteristic!.Category!.ToLower().Contains(query) ||
            product.Characteristic!.Description!.ToLower().Contains(query) ||
            product.Characteristic!.Manufacturer!.ToLower().Contains(query) ||
            product.Characteristic!.Country!.ToLower().Contains(query) ||
            product.Characteristic.ManufactureDate.ToString().ToLower().Contains(query) ||
            product.Characteristic.Status!.ToString().ToLower().Contains(query);
    }
}