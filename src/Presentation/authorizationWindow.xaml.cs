using Business.Services;
using System.Windows;
using System.Windows.Controls;

namespace Presentation;

public partial class AuthorizationWindow : Window
{
    CurrentUserService currentUserService;
    private PasswordService passwordService;

    public AuthorizationWindow()
    {
        currentUserService = new CurrentUserService();
        passwordService = new PasswordService();
        InitializeComponent();
    }

    private void logAccount(object sender, RoutedEventArgs e)
    {
        // TODO: validate

        if (currentUserService.LogIn(LoginTextBox.Text, passwordService.HashString(PasswordBox.Password)))
        {
            ProgramWindow programWindow = new ProgramWindow();
            this.Hide();
            programWindow.ShowDialog();
            this.Close();
        }
    }

    private void forgotPasswordTextBlockMouseDown(object sender, RoutedEventArgs e)
    {
        ChangePassword newPassword = new ChangePassword();
        this.Hide();
        newPassword.ShowDialog();
        this.Close();
    }

    private void registerTextBlockMouseDown(object sender, RoutedEventArgs e)
    {
        RegisterWindow registerWindow = new RegisterWindow();
        this.Hide();
        registerWindow.ShowDialog();
        this.Close();
    }
}
