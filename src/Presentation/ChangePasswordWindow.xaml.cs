using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Business.Services;
using Serilog;


namespace Presentation;

public partial class ChangePassword : Window
{
    private string _email = "";

    public ChangePassword()
    {
        InitializeComponent();
        Log.Information($"{nameof(ChangePassword)}. {AuthorizationService.AuthorizedUser?.Email}. Window opened");
    }

    private void EmailTextBox(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            _email = textBox.Text;
        }
    }

    private void MassageToEmailButton(object sender, RoutedEventArgs e)
    {
    }

    private void ImageMouseDown(object sender, MouseButtonEventArgs e)
    {
        AuthorizationWindow authorizationWindow = new AuthorizationWindow();
        this.Hide();
        authorizationWindow.ShowDialog();
        this.Close();
    }
}