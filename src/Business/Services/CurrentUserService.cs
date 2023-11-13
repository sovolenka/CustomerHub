using Data.Context;
using Data.Models;

namespace Business.Services;

public class CurrentUserService
{
    private static User? _currentUser;
    private SQLiteContext _context = SQLiteContextSingleton.Instance;

    public bool LoggedIn => _currentUser is not null;

    public bool LogIn(string email, byte[] passwordHash)
    {
        foreach(var user in _context.Users!)
        {
            if (user.Email == email && user.PasswordHash!.SequenceEqual(passwordHash))
            {
                _currentUser = user;
                return true;
            }
        }
        return false;
    }

    public bool LogOut()
    {
        if (LoggedIn)
        { 
            _currentUser = null;
            return true;
        }
        return false;
    }
}
