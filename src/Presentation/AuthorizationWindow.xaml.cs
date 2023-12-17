using System;
using Business.Services;
using System.Windows;
using Serilog;

namespace Presentation;

public partial class AuthorizationWindow : Window
{
    private readonly AuthorizationService _authorizationService;
    private readonly PasswordService _passwordService;
    
    public AuthorizationWindow()
    {
        _authorizationService = new AuthorizationService();
        _passwordService = new PasswordService();
        InitializeComponent();
        Log.Logger.Information($"{nameof(AuthorizationWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Window opened");
    }

    private void LogAccount(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(LoginTextBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
        {
            ErrorTextBlock.Text = "Ведіть, будь ласка, пошту та пароль";
            Log.Logger.Warning($"{nameof(AuthorizationWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Empty login or password");
            return;
        }

        if (!_authorizationService.LogIn(LoginTextBox.Text, _passwordService.HashString(PasswordBox.Password)))
        {
            ErrorTextBlock.Text = "Некоректно введено пошту чи пароль";
            Log.Logger.Warning($"{nameof(AuthorizationWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Incorrect login or password");
            return;
        }
        
        ProgramWindow programWindow = new();
        Hide();
        programWindow.ShowDialog();
        Close();
        Log.Logger.Information($"{nameof(AuthorizationWindow)}. {AuthorizationService.AuthorizedUser?.Email}. User {LoginTextBox.Text} logged in");
    }

    private void ForgotPasswordTextBlockMouseDown(object sender, RoutedEventArgs e)
    {
        ForgotPasswordWindow newPassword = new();
        Hide();
        newPassword.ShowDialog();
        Close();
        Log.Logger.Information($"{nameof(AuthorizationWindow)}. {AuthorizationService.AuthorizedUser?.Email}. ForgotPasswordWindow opened");
    }

    private void RegisterTextBlockMouseDown(object sender, RoutedEventArgs e)
    {
        RegisterWindow registerWindow = new();
        Hide();
        registerWindow.ShowDialog();
        Close();
        Log.Logger.Information($"{nameof(AuthorizationWindow)}. {AuthorizationService.AuthorizedUser?.Email}. RegisterWindow opened");
    }
}