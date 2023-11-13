using System.Windows;


namespace Presentation
{

    public partial class ProgramWindow : Window
    {
        public ProgramWindow()
        {
            InitializeComponent();
        }

        private void importClick(object sender, RoutedEventArgs e)
        {

        }

        private void exportClick(object sender, RoutedEventArgs e)
        {

        }

        private void clientListClick(object sender, RoutedEventArgs e)
        {

        }

        private void productListClick(object sender, RoutedEventArgs e)
        {

        }

        private void openClientListWindow(object sender, RoutedEventArgs e)
        {

            ClientListWindow clientListWindow = new ClientListWindow();
            clientListWindow.Show();
        }

        private void openProductListWindow(object sender, RoutedEventArgs e)
        {
            ClientListWindow productListWindow = new ClientListWindow();
            productListWindow.Show();
        }


    }
}
