using System.Windows;
using System.Windows.Controls;

namespace Presentation;

public partial class AuthorizationWindow : Window
{
    private string _email = "";
    private string _password = "";

    private void postTextBox(object sender, RoutedEventArgs e)
    {
        // Зберегти інформацію з TextBox "Пошта:"
        if (sender is TextBox textBox)
        {
            _email = textBox.Text;
        }
    }

    private void passwordTextBox(object sender, RoutedEventArgs e)
    {
        // Зберегти інформацію з PasswordBox "Пароль:"
        if (sender is PasswordBox passwordBox)
        {
            _password = passwordBox.Password;
        }
    }

    private void logAccount(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(_email) && !string.IsNullOrEmpty(_password))
        {
            ProgramWindow programWindow = new ProgramWindow();
            this.Hide();
            programWindow.ShowDialog();
            this.Close();
        }
        else
        {
        }
    }

    private void forgotPasswordTextBlockMouseDown(object sender, RoutedEventArgs e)
    {
        ChangePassword changePassword = new ChangePassword();
        this.Hide();
        changePassword.ShowDialog();
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
