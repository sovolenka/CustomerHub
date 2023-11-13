using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    public partial class RegisterWindow : Window
    {
        private string _email = "";
        private string _password = "";
        private string _confirmPassword = "";
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void newPostTextBox(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                _email = textBox.Text;
            }
        }

        private void newPasswordTextBox(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                _password = passwordBox.Password;
            }
        }

        private void acceptPasswordTextBox(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                _confirmPassword = passwordBox.Password;
            }
        }

        private void createAccountButton(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_email) && !string.IsNullOrWhiteSpace(_password) && _password == _confirmPassword)
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

        private void logAccountMouseDown(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            this.Hide();
            authorizationWindow.ShowDialog();
            this.Close();
        }


    }
}

