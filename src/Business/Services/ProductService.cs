﻿using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class ProductService
{
    private readonly SQLiteContext _context = SQLiteContextSingleton.Instance;

    public ProductService(SQLiteContext context)
    {
        _context = context;
    }
    
    public ProductService()
    {
        _context = SQLiteContextSingleton.Instance;
    }
    
    public Product? Add(Product product, User? user = null)
    {
        if (user is not null) product.User = user;
        Product? added = _context.Products?.Add(product).Entity;
        _context.SaveChanges();
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

    public IEnumerable<Product> GetAllByUser(User user)
    {
        return _context.Products!
            .Include(p => p.Characteristic)
            .Where(p => p.User == user).ToList();
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
               product.Characteristic.ProductStatus!.ToString().ToLower().Contains(query);
    }

    public bool IsProductNameUnique(string? productName, User user)
    {
        if (productName == null)
        {
            return false;
        }
        IEnumerable<Product> existingProducts = GetAllByUser(user);
        return !existingProducts.Any(product =>
            product.Name != null && product.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
    }
}