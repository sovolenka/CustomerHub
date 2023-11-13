using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Presentation
{
    public partial class ChangePassword : Window
    {
        private string _email = "";
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void emailTextBox(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                _email = textBox.Text;
            }
        }
        private void massageToEmailButton(object sender, RoutedEventArgs e)
        {

        }
        private void imageMouseDown(object sender, MouseButtonEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            this.Hide();
            authorizationWindow.ShowDialog();
            this.Close();
        }

    }
}
