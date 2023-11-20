using Business.Services;
using System.Windows;


namespace Presentation
{

    public partial class ProgramWindow : Window
    {
        private CurrentUserService _currentUserService = new();

        public ProgramWindow()
        {
            InitializeComponent();
        }

        private void openEditAccountWindow(object sender, RoutedEventArgs e)
        {
            EditAccountWindow editAccountWindow = new EditAccountWindow();
            editAccountWindow.Show();
        }

        private void logOutClick(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            _currentUserService.LogOut();
            this.Close();
        }

        private void importClick(object sender, RoutedEventArgs e)
        {
            /*
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                // Read the file and process it
                string fileContent = File.ReadAllText(filePath);
                // Add your logic to handle the file content
            }
            */
        }


        private void exportClick(object sender, RoutedEventArgs e)
        {
            /*
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                // Add your logic to get the data you want to export
                string dataToExport = "Your data to export"; // Replace this with your actual data
                File.WriteAllText(filePath, dataToExport);
            }
            */
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
