using Data.Context;
using Data.Models;

namespace Business.Services;

public class UserService
{
    private readonly SQLiteContext _context = SQLiteContextSingleton.Instance;

    public User? Get(int id)
    {
        return _context.Users!.Find(id);
    }
    
    public User? Add(User user)
    {
        if (ContainsEmail(user.Email!)) return null;

        User? added = _context.Users?.Add(user).Entity;
        _context.SaveChanges();
        return added;
    }

    public bool ContainsEmail(string email)
    {
        foreach (var u in _context.Users!)
        {
            if (u.Email == email) return true;
        }

        return false;
    }

    public User? Remove(User user)
    {
        User? removed = _context.Users?.Remove(user).Entity;
        _context.SaveChanges();
        return removed;
    }

    public User? Update(User user)
    {
        User? updated = _context.Users?.Update(user).Entity;
        _context.SaveChanges();
        return updated;
    }
}