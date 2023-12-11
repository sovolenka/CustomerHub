using Business.Services;
using System.Windows;
using Business.Validators;
using Business.Validators.Exceptions;
using Data.Models;
using Serilog;

namespace Presentation;

public partial class UpdatePasswordWindow : Window
{
    private readonly AuthorizationService _authorizationService;
    private readonly UserService _userService;
    private readonly PasswordService _passwordService;

    public UpdatePasswordWindow()
    {
        _authorizationService = new AuthorizationService();
        _userService = new UserService();
        _passwordService = new PasswordService();
        InitializeComponent();
        Log.Information($"{nameof(UpdatePasswordWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Window opened");
    }

    private void ChangePasswordClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(OldPasswordBox.Password) || string.IsNullOrEmpty(NewPasswordBox.Password) ||
            string.IsNullOrEmpty(ConfirmPasswordBox.Password))
        {
            ErrorTextBlock.Text = "Заповніть всі поля";
            return;
        }

        if (!_authorizationService.CheckAuthorizedUserPassword(_passwordService.HashString(OldPasswordBox.Password)))
        {
            ErrorTextBlock.Text = "Неправильний поточний пароль";
            Log.Information($"{nameof(UpdatePasswordWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Incorrect current password");
            return;
        }

        if (OldPasswordBox.Password == NewPasswordBox.Password)
        {
            ErrorTextBlock.Text = "Ваш пароль повинен відрізнятись від поточного";
            Log.Information($"{nameof(UpdatePasswordWindow)}. {AuthorizationService.AuthorizedUser?.Email}. New password is the same as the current one");
            return;
        }
        
        if (NewPasswordBox.Password != ConfirmPasswordBox.Password)
        {
            ErrorTextBlock.Text = "Паролі не співпадають";
            Log.Information($"{nameof(UpdatePasswordWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Passwords do not match");
            return;
        }

        try
        {
            PasswordValidator.Validate(NewPasswordBox.Password);
        }
        catch (InvalidPasswordException)
        {
            ErrorTextBlock.Text = "Придумайте надійніший пароль";
            Log.Information($"{nameof(UpdatePasswordWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Password is not strong enough");
            return;
        }
        
        User? newUser = _userService.Get(AuthorizationService.AuthorizedUser!.Id);
        newUser!.PasswordHash = _passwordService.HashString(NewPasswordBox.Password);
        _userService.Update(newUser);
        Close();
    }
}