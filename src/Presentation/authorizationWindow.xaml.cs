using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for authorizationWindow.xaml
    /// </summary>
    public partial class authorizationWindow : Window
    {

        private void ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void ForgotPasswordTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Логіка для обробки натискання тексту "Забули пароль"
        }

        private void RegisterTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Логіка для обробки натискання тексту "Зареєструватися"
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Логіка для обробки отримання фокусу на полі введення тексту
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Логіка для обробки отримання фокусу на полі введення паролю
        }
            
        public authorizationWindow()
        {
            InitializeComponent();
        }
    }
}
