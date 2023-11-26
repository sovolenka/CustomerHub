using System.Windows;
using System.Windows.Input;

namespace Presentation;

public partial class ForgotPasswordWindow : Window
{
    public ForgotPasswordWindow()
    {
        InitializeComponent();
    }

    private void ImageMouseDown(object sender, MouseButtonEventArgs e)
    {
        AuthorizationWindow authorizationWindow = new AuthorizationWindow();
        Hide();
        authorizationWindow.ShowDialog();
        Close();
    }

    private void SendEmailButton(object sender, MouseButtonEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}