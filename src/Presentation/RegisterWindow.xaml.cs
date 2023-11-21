using System;
using Business.Services;
using Data.Models;
using System.Windows;
using Business.Validators;
using Business.Validators.Exceptions;

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
            return;
        }

        if (PasswordBox.Password != ConfirmPasswordBox.Password)
        {
            ErrorTextBlock.Text = "Пароль не співпадає";
            return;
        }

        try
        {
            PasswordValidator.Validate(PasswordBox.Password);
        }
        catch (InvalidPasswordException)
        {
            ErrorTextBlock.Text = "Придумайте надійніший пароль";
            return;
        }

        User? user = new User(EmailTextBox.Text, _passwordService.HashString(PasswordBox.Password));
        user = _userService.Add(user);

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