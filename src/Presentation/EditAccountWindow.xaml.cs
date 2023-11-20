using Business.Services;
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
    /// Interaction logic for EditAccountWindow.xaml
    /// </summary>
    public partial class EditAccountWindow : Window
    {

        private PasswordService passwordService; 

        public EditAccountWindow()
        {
            InitializeComponent();
            passwordService = new PasswordService();
        }

        private void ChangePasswordClick(object sender, RoutedEventArgs e)
        {
            /*
            string oldPassword = oldPasswordTextBox.Password; 
            string newPassword = newPasswordTextBox.Password; 
            string confirmPassword = confirmPasswordTextBox.Password; 

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Нові паролі не співпадають.");
                return;
            }

            // Припустимо, ChangePassword - це метод у PasswordService, який повертає boolean, вказуючи на успіх чи невдачу
            bool changeSuccessful = passwordService.ChangePassword(oldPassword, newPassword);

            if (changeSuccessful)
            {
                MessageBox.Show("Пароль успішно змінено.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Не вдалося змінити пароль. Будь ласка, перевірте ваш старий пароль.");
            }
            */
        }


    }
}
