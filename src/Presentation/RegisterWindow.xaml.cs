using System;
using Business.Services;
using Data.Models;
using System.Windows;
using Business.Validators;
using Business.Validators.Exceptions;
using Serilog;

namespace Presentation;

public partial class RegisterWindow : Window
{
    private readonly UserService _userService;
    private readonly PasswordService _passwordService;

    public RegisterWindow()
    {
        _userService = new UserService();
        _passwordService = new PasswordService();
        InitializeComponent();
        Log.Information($"{nameof(RegisterWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Window opened");
    }

    private void CreateAccount(object sender, RoutedEventArgs e)
    {
        try
        {
            EmailValidator.Validate(EmailTextBox.Text);
        }
        catch (InvalidEmailException)
        {
            ErrorTextBlock.Text = "Неправильний формат електронної пошти";
            Log.Information($"{nameof(RegisterWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Incorrect email format {EmailTextBox.Text}");
            return;
        }

        if (PasswordBox.Password != ConfirmPasswordBox.Password)
        {
            ErrorTextBlock.Text = "Пароль не співпадає";
            Log.Information($"{nameof(RegisterWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Passwords do not match");
            return;
        }

        try
        {
            PasswordValidator.Validate(PasswordBox.Password);
        }
        catch (InvalidPasswordException)
        {
            ErrorTextBlock.Text = "Придумайте надійніший пароль";
            Log.Information($"{nameof(RegisterWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Password is not strong enough");
            return;
        }

        User? user = new (EmailTextBox.Text, _passwordService.HashString(PasswordBox.Password));
        user = _userService.Add(user);
        Log.Information($"{nameof(RegisterWindow)}. {AuthorizationService.AuthorizedUser?.Email}. User {user?.Email} registered");
        
        if (user is null) return;
        AuthorizationWindow authorizationWindow = new();
        Hide();
        authorizationWindow.ShowDialog();
        Close();
    }

    private void ForgotPasswordTextBlockMouseDown(object sender, RoutedEventArgs e)
    {
    }

    private void LogAccountClick(object sender, RoutedEventArgs e)
    {
        AuthorizationWindow authorizationWindow = new();
        Hide();
        authorizationWindow.ShowDialog();
        Close();
    }
}