using System.Windows;
using System.Windows.Controls;

namespace Presentation;

public partial class AuthorizationWindow : Window
{
    private string email = "";
    private string password = "";

    private void postTextBox(object sender, RoutedEventArgs e)
    {
        // Зберегти інформацію з TextBox "Пошта:"
        if (sender is TextBox textBox)
        {
            email = textBox.Text;
        }
    }

    private void passwordTextBox(object sender, RoutedEventArgs e)
    {
        // Зберегти інформацію з PasswordBox "Пароль:"
        if (sender is PasswordBox passwordBox)
        {
            password = passwordBox.Password;
        }
    }

    private void logAccount(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            ProgramWindow programWindow = new ProgramWindow();
            this.Hide();
            programWindow.ShowDialog();
            this.Close();
        }
        else
        {
            // Обробка ситуації, коли email або password є порожніми
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

    public AuthorizationWindow()
    {
        InitializeComponent();
    }

}
