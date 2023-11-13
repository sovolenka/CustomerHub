using System.Windows;


namespace Presentation
{
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

        }

        private void filterClientClick(object sender, RoutedEventArgs e)
        {

        }

        private void countClientClick(object sender, RoutedEventArgs e)
        {

        }

        private void analysisClientClick(object sender, RoutedEventArgs e)
        {

        }

    }
}
