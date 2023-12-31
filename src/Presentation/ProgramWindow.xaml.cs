using Business.Services;
using Data.Models;
using Presentation.Events;
using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using Serilog;

namespace Presentation;

public partial class ProgramWindow : Window
{
    private readonly AuthorizationService _authorizationService;
    private readonly CsvService _csvService;
    private readonly ClientService _clientService;
    private readonly ProductService _productService;

    public ProgramWindow()
    {
        _authorizationService = new AuthorizationService();
        _csvService = new CsvService();
        _clientService = new ClientService();
        _productService = new ProductService();
        InitializeComponent();
        ClientList.ItemsSource = _clientService.GetAllByUser(AuthorizationService.AuthorizedUser!);
        ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!);
        Log.Information($"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Window opened");
    }

    private void OpenEditAccountWindow(object sender, RoutedEventArgs e)
    {
        UpdatePasswordWindow updateAccountWindow = new();
        updateAccountWindow.Show();
        Log.Information(
            $"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. UpdatePasswordWindow opened");
    }

    private void LogOutClick(object sender, RoutedEventArgs e)
    {
        AuthorizationWindow authorizationWindow = new();
        authorizationWindow.Show();
        _authorizationService.LogOut();
        Close();
        Log.Information(
            $"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. User {AuthorizationService.AuthorizedUser?.Email} logged out");
    }

    private void ImportClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK) return;
            string directory = dialog.SelectedPath;
            _csvService.ImportFromCsv(directory);
            ClientList.ItemsSource = _clientService.GetAllByUser(AuthorizationService.AuthorizedUser!);
            ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!);
            MessageBox.Show("Імпорт завершено");
            Log.Information(
                $"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Import from {directory} completed");
        }
        catch (Exception exception)
        {
            Log.Error($"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Data import error");
            MessageBox.Show("Помилка імпорту даних");
        }
    }

    private void ExportClick(object sender, RoutedEventArgs e)
    {
        // open folder dialog to select directory
        var dialog = new System.Windows.Forms.FolderBrowserDialog();
        var result = dialog.ShowDialog();
        if (result != System.Windows.Forms.DialogResult.OK) return;
        string directory = dialog.SelectedPath;
        _csvService.ExportToCsv(directory);
        MessageBox.Show("Експорт завершено");
        Log.Information(
            $"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Export to {directory} completed");
    }


    private void AnalysisClick(object sender, RoutedEventArgs e)
    {
        AnalysisWindow analysisWindow = new();
        analysisWindow.Show();
        Log.Information(
            $"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. AnalysisWindow opened");
    }

    private void ShowClientList(object sender, RoutedEventArgs e)
    {
        productsMenuItem.Visibility = Visibility.Visible;
        clientMenuItem.Visibility = Visibility.Collapsed;
        listClients.Visibility = Visibility.Visible;
        listProducts.Visibility = Visibility.Collapsed;
        Log.Information($"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Client list opened");
    }

    private void ShowProductList(object sender, RoutedEventArgs e)
    {
        clientMenuItem.Visibility = Visibility.Visible;
        productsMenuItem.Visibility = Visibility.Collapsed;
        listProducts.Visibility = Visibility.Visible;
        listClients.Visibility = Visibility.Collapsed;
        Log.Information($"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Product list opened");
    }

    private void OpenAddClientWindow(object sender, RoutedEventArgs e)
    {
        AddClientWindow addClientWindow = new();
        addClientWindow.ClientAdded += OnClientsUpdate;
        addClientWindow.Show();
        Log.Information(
            $"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. AddClientWindow opened");
    }

    private void OnClientsUpdate(object? sender, EntityEventArgs e)
    {
        ClientList.ItemsSource = _clientService.GetAllByUser(AuthorizationService.AuthorizedUser!);
        Log.Information($"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Client list updated");
    }

    private void OnPredicateUpdate(object? sender, EntityPredicateEventArgs e)
    {
        Predicate<Client> predicate = e.ClientPredicate;

        ClientList.ItemsSource = _clientService.GetAllByUser(AuthorizationService.AuthorizedUser!)
            .Where(client => predicate(client));
        Log.Information($"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Client list updated");
    }

    private void OpenEditClientWindow(object sender, RoutedEventArgs e)
    {
        Client selectedClient = (Client)ClientList.SelectedItem;
        UpdateClientWindow updateClientWindow = new(selectedClient);
        updateClientWindow.ClientAdded += OnClientsUpdate;
        updateClientWindow.Show();
        Log.Information(
            $"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. UpdateClientWindow opened");
    }

    private void DeleteClientClick(object sender, RoutedEventArgs e)
    {
        Client selectedClient = (Client)ClientList.SelectedItem;
        MessageBoxResult messageBoxResult = MessageBox.Show(
            $"Ви впевнені, що хочете видалити клієнта {selectedClient.FirstName} {selectedClient.SecondName}?",
            "Підтвердження видалення",
            MessageBoxButton.YesNo);
        if (messageBoxResult == MessageBoxResult.No) return;
        _clientService.Remove(selectedClient);
        Log.Information(
            $"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Client {selectedClient.FirstName} {selectedClient.SecondName} deleted");
        ClientList.ItemsSource = _clientService.GetAllByUser(AuthorizationService.AuthorizedUser!);
        Log.Information($"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Client list updated");
    }

    private void SearchClientClick(object sender, RoutedEventArgs e)
    {
        SearchWindow searchWindow = new();
        searchWindow.SearchApplied += OnPredicateUpdate;
        searchWindow.Show();
        searchClient.Visibility = Visibility.Collapsed;
        allClients.Visibility = Visibility.Visible;
        Log.Information($"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. SearchWindow opened");
    }

    private void AllClientsClick(object sender, RoutedEventArgs e)
    {
        allClients.Visibility = Visibility.Hidden;
        searchClient.Visibility = Visibility.Visible;
        ClientList.ItemsSource = _clientService.GetAllByUser(AuthorizationService.AuthorizedUser!);
        Log.Information($"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Client list updated");
    }

    private void OpenAddProductWindow(object sender, RoutedEventArgs e)
    {
        AddProductWindow addProductWindow = new();
        addProductWindow.ProductAdded += OnProductsUpdate;
        addProductWindow.Show();
        Log.Information(
            $"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. AddProductWindow opened");
    }

    // Event handler for the ClientAdded event
    private void OnProductsUpdate(object? sender, EntityEventArgs e)
    {
        ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!);
        Log.Information($"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Product list updated");
    }

    private void OnPredicateUpdateProducts(object? sender, EntityPredicateEventArgs e)
    {
        Predicate<Product>? predicate = e.ProductPredicate;

        ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!)
            .Where(client => predicate(client));
        Log.Information($"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Product list updated");
    }

    private void DeleteProductClick(object sender, RoutedEventArgs e)
    {
        Product selectedProduct = (Product)ProductList.SelectedItem;
        MessageBoxResult messageBoxResult = MessageBox.Show(
            $"Ви впевнені, що хочете видалити продукт {selectedProduct.Name}?",
            "Підтвердження видалення",
            MessageBoxButton.YesNo);
        if (messageBoxResult == MessageBoxResult.No) return;
        _productService.Remove(selectedProduct);
        Log.Information(
            $"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Product {selectedProduct.Name} deleted");
        ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!);
        Log.Information($"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Product list updated");
    }

    private void SearchProductsClick(object sender, RoutedEventArgs e)
    {
        searchProduct.Visibility = Visibility.Collapsed;
        allProduct.Visibility = Visibility.Visible;
        SearchWindow searchWindow = new();
        searchWindow.SearchApplied += OnPredicateUpdateProducts;
        searchWindow.Show();
        Log.Information($"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. SearchWindow opened");
    }

    private void AllProductsClick(object sender, RoutedEventArgs e)
    {
        ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!);
        allProduct.Visibility = Visibility.Collapsed;
        searchProduct.Visibility = Visibility.Visible;
        Log.Information($"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Product list updated");
    }

    private void AddProductClick(object sender, RoutedEventArgs e)
    {
        AddProductWindow addProductWindow = new();
        addProductWindow.ProductAdded += OnProductsUpdate;
        addProductWindow.Show();
        Log.Information(
            $"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. AddProductWindow opened");
    }

    private void EditProductClick(object sender, RoutedEventArgs e)
    {
        Product selectedProduct = (Product)ProductList.SelectedItem;
        UpdateProductWindow updateClientWindow = new(selectedProduct);
        updateClientWindow.ProductAdded += OnProductsUpdate;
        updateClientWindow.Show();
        Log.Information(
            $"{nameof(ProgramWindow)}. {AuthorizationService.AuthorizedUser?.Email}. UpdateProductWindow opened");
    }

    private void ClientListSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ClientList.SelectedItem is null)
        {
            editClient.Visibility = Visibility.Collapsed;
            deleteClient.Visibility = Visibility.Collapsed;
        }
        else
        {
            editClient.Visibility = Visibility.Visible;
            deleteClient.Visibility = Visibility.Visible;
        }
    }

    private void ProductListSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ProductList.SelectedItem is null)
        {
            editProduct.Visibility = Visibility.Collapsed;
            deleteProduct.Visibility = Visibility.Collapsed;
        }
        else
        {
            editProduct.Visibility = Visibility.Visible;
            deleteProduct.Visibility = Visibility.Visible;
        }
    }
}