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
    /// Interaction logic for ClientListWindow.xaml
    /// </summary>
    public partial class ClientListWindow : Window
    {
        public ClientListWindow()
        {
            InitializeComponent();
        }

        private void openAddClientWindow(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();

            addClientWindow.Show();
        }

        private void openEditClientWindow(object sender, RoutedEventArgs e)
        {
            EditClientWindow EditClientWindow = new EditClientWindow();

            EditClientWindow.Show();
        }

        private void deleteClientClick(object sender, RoutedEventArgs e)
        {
            // Code to handle deleting a client
        }

        private void filterClientClick(object sender, RoutedEventArgs e)
        {
            // Code to handle filtering the client list
        }

        private void countClientClick(object sender, RoutedEventArgs e)
        {
            // Code to handle counting clients
        }

        private void analysisClientClick(object sender, RoutedEventArgs e)
        {
            // Code to handle analyzing client data
        }

    }
}
