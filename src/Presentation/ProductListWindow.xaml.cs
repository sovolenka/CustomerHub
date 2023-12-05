using Business.Services;
using Data.Models;
using Presentation.Events;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Presentation;

public partial class ProductListWindow : Window
{
    private readonly ProductService _productService;

    public ProductListWindow()
    {
        _productService = new ProductService();
        InitializeComponent();
        ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!);
    }

    private void OpenAddProductWindow(object sender, RoutedEventArgs e)
    {
        AddProductWindow addProductWindow = new ();
        addProductWindow.ProductAdded += OnProductsUpdate;
        addProductWindow.Show();
    }

    // Event handler for the ClientAdded event
    private void OnProductsUpdate(object? sender, EntityEventArgs e)
    {
        ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!);
    }

    private void OnPredicateUpdate(object? sender, EntityPredicateEventArgs e)
    {
        Predicate<Product>? predicate = e.ProductPredicate;

        ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!)
            .Where(client => predicate(client));
    }

    private void OpenEditProductWindow(object sender, RoutedEventArgs e)
    {
        if (ProductList.SelectedItem is null)
        {
            MessageBox.Show("Виберіть продукт для редагування");
            return;
        }

        Product selectedProduct = (Product)ProductList.SelectedItem;
        UpdateProductWindow updateClientWindow = new (selectedProduct);
        updateClientWindow.ProductAdded += OnProductsUpdate;
        updateClientWindow.Show();
    }

    private void DeleteProductClick(object sender, RoutedEventArgs e)
    {
        if (ProductList.SelectedItem is null)
        {
            MessageBox.Show("Виберіть продукт для видалення");
            return;
        }

        Product selectedProduct = (Product)ProductList.SelectedItem;
        MessageBoxResult messageBoxResult = MessageBox.Show(
            $"Ви впевнені, що хочете видалити {selectedProduct.Name}?",
            "Delete Confirmation", MessageBoxButton.YesNo);
        if (messageBoxResult == MessageBoxResult.No) return;
        _productService.Remove(selectedProduct);
        ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!);
    }

    private void SearchProductClick(object sender, RoutedEventArgs e)
    {
        SearchWindow searchWindow = new();
        searchWindow.SearchApplied += OnPredicateUpdate;
        searchWindow.Show();
    }

    private void AllProductsClick(object sender, RoutedEventArgs e)
    {
        ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!);
    }

    private void AddProductClick(object sender, RoutedEventArgs e)
    {
        AddProductWindow addProductWindow = new ();
        addProductWindow.ProductAdded += OnProductsUpdate;
        addProductWindow.Show();
    }

    private void SearchClientClick(object sender, RoutedEventArgs e)
    {
        SearchWindow searchWindow = new();
        searchWindow.Show();
    }
    private void EditProductClick(object sender, RoutedEventArgs e)
    {
        if (ProductList.SelectedItem is null)
        {
            MessageBox.Show("Виберіть продукт для редагування");
            return;
        }

        Product selectedProduct = (Product)ProductList.SelectedItem;
        UpdateProductWindow updateClientWindow = new (selectedProduct);
        updateClientWindow.ProductAdded += OnProductsUpdate;
        updateClientWindow.Show();
    }


    private void AllProductClick(object sender, RoutedEventArgs e)
    {
        ProductList.ItemsSource = _productService.GetAllByUser(AuthorizationService.AuthorizedUser!);
    }

}