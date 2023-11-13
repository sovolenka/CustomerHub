using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    public partial class RegisterWindow : Window
    {
        private string email = "";
        private string password = "";
        private string confirmPassword = "";
        public RegisterWindow()
        {
            InitializeComponent();
        }



        private void newPostTextBox(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                email = textBox.Text;
            }
        }

        private void newPasswordTextBox(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                password = passwordBox.Password;
            }
        }

        private void acceptPasswordTextBox(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                confirmPassword = passwordBox.Password;
            }
        }

        private void createAccount(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password) && password == confirmPassword)
            {
                // логіка створення акаунту
                ProgramWindow programWindow = new ProgramWindow();
                this.Hide();
                programWindow.ShowDialog();
                this.Close();
            }
            else
            {
                // Обробка випадку, коли дані не введені або паролі не співпадають
            }
        }

        private void forgotPasswordTextBlockMouseDown(object sender, RoutedEventArgs e)
        {
            // Код для події MouseDown для TextBlock "Забули пароль?"
        }

        private void logAccountMouseDown(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            this.Hide();
            authorizationWindow.ShowDialog();
            this.Close();
        }
    }
}

