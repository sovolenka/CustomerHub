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
    /// Interaction logic for AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
        }

        private void addButtonClick(object sender, RoutedEventArgs e)
        {
            // Код для додавання нового клієнта

            // MessageBox.Show("Клієнт збережений!");
        }

        private void cancelButtonClick(object sender, RoutedEventArgs e)
        {
           // this.Close(); 
        }

    }
}
