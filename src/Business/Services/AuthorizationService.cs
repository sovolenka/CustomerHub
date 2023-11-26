using Data.Context;
using Data.Models;

namespace Business.Services;

public class AuthorizationService
{
    private readonly SQLiteContext _context = SQLiteContextSingleton.Instance;
    
    public static bool LoggedIn => AuthorizedUser is not null;
    public static User? AuthorizedUser { get; private set; }

    public void UpdateAuthorizedUser()
    {
        if (!LoggedIn) return;
        AuthorizedUser = _context.Users!.Find(AuthorizedUser!.Id);
    }
    
    public bool LogIn(string email, byte[] passwordHash)
    {
        foreach (var user in _context.Users!)
        {
            if (user.Email != email || !user.PasswordHash!.SequenceEqual(passwordHash)) continue;
            AuthorizedUser = user;
            return true;
        }

        return false;
    }

    public bool LogOut()
    {
        if (!LoggedIn) return false;
        AuthorizedUser = null;
        return true;
    }
    
    public bool CheckAuthorizedUserPassword(byte[] passwordHash)
    {
        return LoggedIn && AuthorizedUser!.PasswordHash!.SequenceEqual(passwordHash);
    }
}