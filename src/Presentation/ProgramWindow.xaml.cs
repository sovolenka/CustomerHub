using Business.Services;
using System.Windows;


namespace Presentation;

public partial class ProgramWindow : Window
{
    private readonly AuthorizationService _authorizationService;

    public ProgramWindow()
    {
        _authorizationService = new AuthorizationService();
        InitializeComponent();
    }

    private void OpenEditAccountWindow(object sender, RoutedEventArgs e)
    {
        EditPasswordWindow editAccountWindow = new();
        editAccountWindow.Show();
    }

    private void LogOutClick(object sender, RoutedEventArgs e)
    {
        AuthorizationWindow authorizationWindow = new();
        authorizationWindow.Show();
        _authorizationService.LogOut();
        Close();
    }

    private void ImportClick(object sender, RoutedEventArgs e)
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


    private void ExportClick(object sender, RoutedEventArgs e)
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


    private void OpenClientListWindow(object sender, RoutedEventArgs e)
    {
        ClientListWindow clientListWindow = new();
        clientListWindow.Show();
    }

    private void OpenProductListWindow(object sender, RoutedEventArgs e)
    {
        ClientListWindow productListWindow = new();
        productListWindow.Show();
    }
}