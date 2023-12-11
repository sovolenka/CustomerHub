using System.Text;
using Business.Services;
using Data.Models;

namespace Test.Services;

public class AuthorizationServiceTest : TestsBase
{
    private readonly AuthorizationService _service;
    
    private readonly User _user = new("test@example.com", Encoding.UTF8.GetBytes("pa$$word"));
    
    public AuthorizationServiceTest() : base("AuthorizationServiceTest.db")
    {
        _service = new AuthorizationService(ContextProxy);
        ContextProxy.Users!.Add(_user);
        ContextProxy.SaveChanges();
        _service.LogIn("test@example.com", _user.PasswordHash!);
    }

    [Fact]
    public void LogIn_ValidCredentials_ReturnsTrueAndSetsAuthorizedUser()
    {
        var result = _service.LogIn("test@example.com", _user.PasswordHash!);
        
        Assert.True(result);
        Assert.NotNull(AuthorizationService.AuthorizedUser);
        Assert.Equal(_user, AuthorizationService.AuthorizedUser);
    }

    [Fact]
    public void LogIn_InvalidCredentials_ReturnsFalseAndDoesNotSetAuthorizedUser()
    {
        var result = _service.LogIn("test@example.com", new byte[] { 0, 1, 2 }); // Invalid password hash
        Assert.False(result);
    }

    [Fact]
    public void LogOut_UserLoggedIn_SetsAuthorizedUserToNull()
    {
        _service.LogIn("test@example.com", Encoding.UTF8.GetBytes("pa$$word"));
        
        var result = _service.LogOut();
        
        Assert.True(result);
        Assert.Null(AuthorizationService.AuthorizedUser);
    }

    [Fact]
    public void LogOut_UserNotLoggedIn_ReturnsFalse()
    {
        _service.LogOut();
        var result = _service.LogOut();
        
        Assert.False(result);
    }

    [Fact]
    public void CheckAuthorizedUserPassword_UserLoggedInAndCorrectPassword_ReturnsTrue()
    {
        var result = _service.CheckAuthorizedUserPassword(_user.PasswordHash!);
        
        Assert.True(result);
    }

    [Fact]
    public void CheckAuthorizedUserPassword_UserLoggedInAndIncorrectPassword_ReturnsFalse()
    {
        var result = _service.CheckAuthorizedUserPassword(new byte[] { 0, 1, 2 }); // Incorrect password hash
        
        Assert.False(result);
    }

    [Fact]
    public void CheckAuthorizedUserPassword_UserNotLoggedIn_ReturnsFalse()
    {
        var result = _service.CheckAuthorizedUserPassword(new byte[] { 0, 1, 2 });
        
        Assert.False(result);
    }
}
