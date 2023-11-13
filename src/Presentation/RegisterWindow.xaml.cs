using Business.Services;
using Data.Models;
using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    public partial class RegisterWindow : Window
    {
        private UserService userService;
        private PasswordService passwordService;
        private CurrentUserService currentUserService;

        public RegisterWindow()
        {
            userService = new UserService();
            passwordService = new PasswordService();
            currentUserService = new CurrentUserService();  
            InitializeComponent();
        }

        private void createAccount(object sender, RoutedEventArgs e)
        {
            // TODO: validate password via Validator

            User? user = new User(LoginTextBox.Text, passwordService.HashString(PasswordBox.Password));
            user = userService.Add(user);
            
            if (user is not null)
            {
                AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                Hide();
                authorizationWindow.ShowDialog();
                Close();
            }
        }

        private void forgotPasswordTextBlockMouseDown(object sender, RoutedEventArgs e)
        {

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
