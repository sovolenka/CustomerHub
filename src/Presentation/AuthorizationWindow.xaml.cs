using Business.Services;
using System.Windows;

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
    }

    private void LogAccount(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(LoginTextBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
        {
            ErrorTextBlock.Text = "Ведіть, будь ласка, пошту та пароль";
            return;
        }

        if (!_authorizationService.LogIn(LoginTextBox.Text, _passwordService.HashString(PasswordBox.Password)))
        {
            ErrorTextBlock.Text = "Некоректно введено пошту чи пароль";
            return;
        }
        
        ProgramWindow programWindow = new();
        Hide();
        programWindow.ShowDialog();
        Close();
    }

    private void ForgotPasswordTextBlockMouseDown(object sender, RoutedEventArgs e)
    {
        ForgotPasswordWindow newPassword = new();
        Hide();
        newPassword.ShowDialog();
        Close();
    }

    private void RegisterTextBlockMouseDown(object sender, RoutedEventArgs e)
    {
        RegisterWindow registerWindow = new();
        Hide();
        registerWindow.ShowDialog();
        Close();
    }
}