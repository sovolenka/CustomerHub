using System.Windows;


namespace Presentation
{
    public partial class ClientListWindow : Window
    {
        public ClientListWindow()
        {
            InitializeComponent();
        }

        private void OpenAddClientWindow(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();

            addClientWindow.Show();
        }

        private void OpenEditClientWindow(object sender, RoutedEventArgs e)
        {
            EditClientWindow editClientWindow = new EditClientWindow();

            editClientWindow.Show();
        }

        private void DeleteClientClick(object sender, RoutedEventArgs e)
        {
        }

        private void FilterClientClick(object sender, RoutedEventArgs e)
        {
        }

        private void CountClientClick(object sender, RoutedEventArgs e)
        {
        }

        private void AnalysisClientClick(object sender, RoutedEventArgs e)
        {
        }
    }
}