using Business.Services;
using Data.Models;
using Presentation.Events;
using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;

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


    private void AnalysisClick(object sender, RoutedEventArgs e)
    {
        AnalysisWindow analysisWindow = new();
        analysisWindow.Show();
    }

    private void ShowClientList(object sender, RoutedEventArgs e)
    {
        productsMenuItem.Visibility = Visibility.Visible;
        clientMenuItem.Visibility = Visibility.Collapsed;
        listClients.Visibility = Visibility.Visible;
        listProducts.Visibility = Visibility.Collapsed;
    }

    private void ShowProductList(object sender, RoutedEventArgs e)
    {
        clientMenuItem.Visibility = Visibility.Visible;
        productsMenuItem.Visibility = Visibility.Collapsed;
        listProducts.Visibility = Visibility.Visible;
        listClients.Visibility = Visibility.Collapsed;
    }

    private void OpenAddClientWindow(object sender, RoutedEventArgs e)
    {
        AddClientWindow addClientWindow = new();
        addClientWindow.ClientAdded += OnClientsUpdate;
        addClientWindow.Show();
    }

    private void OnClientsUpdate(object? sender, EntityEventArgs e)
    {
        ClientList.ItemsSource = _clientService.GetAllByUser(AuthorizationService.AuthorizedUser!);
    }

    private void OnPredicateUpdate(object? sender, EntityPredicateEventArgs e)
    {
        Predicate<Client> predicate = e.ClientPredicate;

        ClientList.ItemsSource = _clientService.GetAllByUser(AuthorizationService.AuthorizedUser!)
            .Where(client => predicate(client));
    }

    private void OpenEditClientWindow(object sender, RoutedEventArgs e)
    {
        Client selectedClient = (Client)ClientList.SelectedItem;
        UpdateClientWindow updateClientWindow = new(selectedClient);
        updateClientWindow.ClientAdded += OnClientsUpdate;
        updateClientWindow.Show();
    }

    private void DeleteClientClick(object sender, RoutedEventArgs e)
    {
        Client selectedClient = (Client)ClientList.SelectedItem;
        MessageBoxResult messageBoxResult = MessageBox.Show(
            $"Ви впевнені, що хочете видалити {selectedClient.FirstName} {selectedClient.SecondName}?",
            "Delete Confirmation", MessageBoxButton.YesNo);
        if (messageBoxResult == MessageBoxResult.No) return;
        _clientService.Remove(selectedClient);
        ClientList.ItemsSource = _clientService.GetAllByUser(AuthorizationService.AuthorizedUser!);
    }

    private void SearchClientClick(object sender, RoutedEventArgs e)
    {
        SearchWindow searchWindow = new();
        searchWindow.SearchApplied += OnPredicateUpdate;
        searchWindow.Show();
        searchClient.Visibility = Visibility.Collapsed;
        allClients.Visibility = Visibility.Visible;
    }

    private void AllClientsClick(object sender, RoutedEventArgs e)
    {
        allClients.Visibility = Visibility.Hidden;
        searchClient.Visibility = Visibility.Visible;
        ClientList.ItemsSource = _clientService.GetAllByUser(AuthorizationService.AuthorizedUser!);
    }

    private void OpenAddProductWindow(object sender, RoutedEventArgs e)
    {
        AddProductWindow addProductWindow = new();
        addProductWindow.ProductAdded += OnProductsUpdate;
        addProductWindow.Show();
    }

    // Event handler for the ClientAdded event
    private void OnProductsUpdate(object? sender, EntityEventArgs e)
    {
        ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!);
    }

    private void OnPredicateUpdateProducts(object? sender, EntityPredicateEventArgs e)
    {
        Predicate<Product>? predicate = e.ProductPredicate;

        ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!)
            .Where(client => predicate(client));
    }

    private void DeleteProductClick(object sender, RoutedEventArgs e)
    {
        Product selectedProduct = (Product)ProductList.SelectedItem;
        MessageBoxResult messageBoxResult = MessageBox.Show(
            $"Ви впевнені, що хочете видалити {selectedProduct.Name}?",
            "Delete Confirmation", MessageBoxButton.YesNo);
        if (messageBoxResult == MessageBoxResult.No) return;
        _productService.Remove(selectedProduct);
        ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!);
    }

    private void SearchProductsClick(object sender, RoutedEventArgs e)
    {
        searchProduct.Visibility = Visibility.Collapsed;
        allProduct.Visibility = Visibility.Visible;
        SearchWindow searchWindow = new();
        searchWindow.SearchApplied += OnPredicateUpdateProducts;
        searchWindow.Show();
    }

    private void AllProductsClick(object sender, RoutedEventArgs e)
    {
        ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!);
        allProduct.Visibility = Visibility.Collapsed;
        searchProduct.Visibility = Visibility.Visible;
    }

    private void AddProductClick(object sender, RoutedEventArgs e)
    {
        AddProductWindow addProductWindow = new();
        addProductWindow.ProductAdded += OnProductsUpdate;
        addProductWindow.Show();
    }

    private void EditProductClick(object sender, RoutedEventArgs e)
    {
        Product selectedProduct = (Product)ProductList.SelectedItem;
        UpdateProductWindow updateClientWindow = new(selectedProduct);
        updateClientWindow.ProductAdded += OnProductsUpdate;
        updateClientWindow.Show();
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