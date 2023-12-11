using System;
using System.Windows;
using Business.Services;
using Data.Models;
using Presentation.Events;
using Serilog;

namespace Presentation;

public partial class AddProductWindow : Window
{
    private readonly ProductService _productService;
    private readonly TimeService _timeService;
    public event EventHandler<EntityEventArgs> ProductAdded;

    public AddProductWindow()
    {
        _productService = new ProductService();
        _timeService = new TimeService();
        ProductAdded += (sender, args) => { };
        InitializeComponent();
        InitializeStatusComboBox();
        InitializeDatePicker();
        ClearFields();
        Log.Information($"{nameof(AddProductWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Window opened.");
    }

    private void InitializeStatusComboBox()
    {
        var statuses = Enum.GetValues(typeof(ProductStatus));
        foreach (var status in statuses)
        {
            StatusComboBox.Items.Add(status);
        }
    }

    private void InitializeDatePicker()
    {
        // set maximum date to today
        DatePicker.DisplayDateEnd = _timeService.GetCurrentDateTime();
    }

    private void ClearFields()
    {
        NameTextBox.Text = "";
        PriceTextBox.Text = "";
        TypeTextBox.Text = "";
        CategoryTextBox.Text = "";
        DescriptionTextBox.Text = "";
        ManufacturerTextBox.Text = "";
        CountryTextBox.Text = "";
    }

    private void AddButtonClick(object sender, RoutedEventArgs e)
    {
        Characteristic characteristic = new Characteristic(
            TypeTextBox.Text,
            CategoryTextBox.Text,
            DescriptionTextBox.Text,
            ManufacturerTextBox.Text,
            CountryTextBox.Text,
            DateOnly.FromDateTime(DateTime.Now),
            (ProductStatus)StatusComboBox.SelectedItem!
        );
        Product product = new Product(
            NameTextBox.Text,
            Convert.ToInt32(PriceTextBox.Text),
            characteristic
        );

        Product? added = _productService.Add(product, AuthorizationService.AuthorizedUser!);
        if (added is null)
        {
            ErrorTextBlock.Text = "Помилка при додаванні продукту";
            Log.Error($"{nameof(AddProductWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Error while adding product: {product.Name}");
        }
        else
        {
            OnProductAdded(new EntityEventArgs(added));
            ErrorTextBlock.Text = "Продукт успішно додано";
            Log.Information($"{nameof(AddProductWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Product added: {product.Name}");
            ClearFields();
        }
    }

    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
        Log.Information($"{nameof(AddProductWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Window closed.");
    }

    protected virtual void OnProductAdded(EntityEventArgs e)
    {
        ProductAdded?.Invoke(this, e);
    }
}