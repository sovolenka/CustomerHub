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
    /// Interaction logic for ProgramWindow.xaml
    /// </summary>
    public partial class ProgramWindow : Window
    {
        public ProgramWindow()
        {
            InitializeComponent();
        }

        private void importClick(object sender, RoutedEventArgs e)
        {
            // Код для імпортування даних
        }

        private void exportClick(object sender, RoutedEventArgs e)
        {
            // Код для експортування даних
        }

        private void clientListClick(object sender, RoutedEventArgs e)
        {
            // Код для вікна списку клієнтів
        }

        private void productListClick(object sender, RoutedEventArgs e)
        {
            // Код для вікна списку товарів
        }

        private void openClientListWindow(object sender, RoutedEventArgs e)
        {
            ClientListWindow clientListWindow = new ClientListWindow();
            clientListWindow.Owner = this; // Це встановлює головне вікно як батьківське для нового вікна
            clientListWindow.ShowDialog(); // Показати вікно як модальне діалогове вікно
        }


    }
}
