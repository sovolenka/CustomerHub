using System.Windows;
using System.Windows.Input;

namespace Presentation;

public partial class AuthorizationWindow : Window
{

    private void logAccount(object sender, RoutedEventArgs e)
    {

    }

    private void forgotPasswordTextBlockMouseDown(object sender, MouseButtonEventArgs e)
    {
        // Логіка для обробки натискання тексту "Забули пароль"
    }

    private void registerTextBlockMouseDown(object sender, MouseButtonEventArgs e)
    {
        // Логіка для обробки натискання тексту "Зареєструватися"
    }

    private void textBoxGotFocus(object sender, RoutedEventArgs e)
    {
        // Логіка для обробки отримання фокусу на полі введення тексту
    }

    private void passwordBoxGotFocus(object sender, RoutedEventArgs e)
    {
        // Логіка для обробки отримання фокусу на полі введення паролю
    }
        
    public AuthorizationWindow()
    {
        InitializeComponent();
    }

}
