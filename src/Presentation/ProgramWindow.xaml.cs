using Business.Services;
using System.Windows;
using Business.IO;


namespace Presentation;

public partial class ProgramWindow : Window
{
    private readonly AuthorizationService _authorizationService;
    private readonly CsvService _csvService;

    public ProgramWindow()
    {
        _authorizationService = new AuthorizationService();
        _csvService = new CsvService();
        InitializeComponent();
    }

    private void OpenEditAccountWindow(object sender, RoutedEventArgs e)
    {
        UpdatePasswordWindow updateAccountWindow = new();
        updateAccountWindow.Show();
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
        // open folder dialog to select directory
        var dialog = new System.Windows.Forms.FolderBrowserDialog();
        var result = dialog.ShowDialog();
        if (result != System.Windows.Forms.DialogResult.OK) return;
        string directory = dialog.SelectedPath;
        _csvService.ImportFromCsv(directory);
    }

    private void ExportClick(object sender, RoutedEventArgs e)
    {
        // open folder dialog to select directory
        var dialog = new System.Windows.Forms.FolderBrowserDialog();
        var result = dialog.ShowDialog();
        if (result != System.Windows.Forms.DialogResult.OK) return;
        string directory = dialog.SelectedPath;
        _csvService.ExportToCsv(directory);
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