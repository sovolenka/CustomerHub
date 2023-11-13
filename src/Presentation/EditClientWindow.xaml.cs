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
    /// Interaction logic for EditClientWindow.xaml
    /// </summary>
    public partial class EditClientWindow : Window
    {
        public EditClientWindow()
        {
            InitializeComponent();
        }

        private void editButtonClick(object sender, RoutedEventArgs e)
        {
            // Код для обробки натискання кнопки "Редагувати"

            // MessageBox.Show("Зміни збережено!");
        }

        private void cancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}
