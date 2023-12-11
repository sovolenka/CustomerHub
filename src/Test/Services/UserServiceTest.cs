using System.Text;
using Business.Services;
using Data.Models;

namespace Test.Services;

public class UserServiceTest : TestsBase
{
    private readonly UserService _userService;

    public UserServiceTest() : base("UserServiceTest.db")
    {
        _userService = new UserService(ContextProxy);
        DeleteUsers();
    }

    private void DeleteUsers()
    {
        ContextProxy.Users!.RemoveRange(ContextProxy.Users!);
        ContextProxy.SaveChanges();
    }

    [Fact]
    public void Get_ExistingUser_ReturnsUser()
    {
        var user = new User("test@example.com", Encoding.UTF8.GetBytes("pa$$word"));
        user = ContextProxy.Users!.Add(user).Entity;
        ContextProxy.SaveChanges();

        var result = _userService.Get(user.Id);

        Assert.NotNull(result);
        Assert.Equal(user.Email, result?.Email);
        Assert.Equal(user.PasswordHash, result?.PasswordHash);
    }

    [Fact]
    public void Get_NonExistingUser_ReturnsNull()
    {
        var nonExistingUserId = 999; // Assuming 999 is a non-existing user ID

        var result = _userService.Get(nonExistingUserId);

        Assert.Null(result);
    }

    [Fact]
    public void Add_NewUser_AddsUserToDatabase()
    {
        var newUser = new User("newuser@example.com", Encoding.UTF8.GetBytes("newpassword"));

        var addedUser = _userService.Add(newUser);

        Assert.NotNull(addedUser);
        Assert.Equal(newUser, addedUser);
        Assert.Contains(addedUser, ContextProxy.Users!);
    }

    [Fact]
    public void Add_DuplicateEmail_ReturnsNull()
    {
        var existingUser = new User("existing@example.com", Encoding.UTF8.GetBytes("existingpassword"));
        ContextProxy.Users!.Add(existingUser);
        ContextProxy.SaveChanges();

        var duplicateUser = new User("existing@example.com", Encoding.UTF8.GetBytes("newpassword"));

        var addedUser = _userService.Add(duplicateUser);

        Assert.Null(addedUser);
    }

    [Fact]
    public void Remove_ExistingUser_RemovesUserFromDatabase()
    {
        var userToRemove = new User("remove@example.com", Encoding.UTF8.GetBytes("removepassword"));
        ContextProxy.Users!.Add(userToRemove);
        ContextProxy.SaveChanges();

        var removedUser = _userService.Remove(userToRemove);

        Assert.NotNull(removedUser);
        Assert.Equal(userToRemove, removedUser);
        Assert.DoesNotContain(userToRemove, ContextProxy.Users!);
    }


    [Fact]
    public void Update_ExistingUser_UpdatesUserInDatabase()
    {
        var userToUpdate = new User("update@example.com", Encoding.UTF8.GetBytes("updatepassword"));
        userToUpdate = ContextProxy.Users!.Add(userToUpdate).Entity;
        ContextProxy.SaveChanges();

        userToUpdate.Email = "updated@example.com";
        userToUpdate.PasswordHash = Encoding.UTF8.GetBytes("updatedpassword");

        var result = _userService.Update(userToUpdate);

        Assert.NotNull(result);
        Assert.Equal(userToUpdate.Email, result?.Email);
        Assert.Equal(userToUpdate.PasswordHash, result?.PasswordHash);
        var updatedFromDb = ContextProxy.Users!.Find(userToUpdate.Id);
        Assert.Equal(userToUpdate.Email, updatedFromDb?.Email);
    }
}