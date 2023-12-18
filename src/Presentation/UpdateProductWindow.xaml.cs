using Data.Models;
using Presentation.Events;
using System;
using System.Windows;
using Business.Services;
using System.Windows.Media;
using Serilog;

namespace Presentation;


public partial class UpdateProductWindow : Window
{
    private Product _product;
    private readonly ProductService _productService;
    public event EventHandler<EntityEventArgs> ProductAdded;

    public UpdateProductWindow(Product product)
    {
        _product = product;
        _productService = new ProductService();
        ProductAdded += (sender, args) => { };
        InitializeComponent();
        InitializeStatusComboBox();
        InitializeFields();
        Log.Information($"{nameof(UpdateProductWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Window opened");
    }

    private void InitializeStatusComboBox()
    {
        var statuses = Enum.GetValues(typeof(ProductStatus));
        foreach (var status in statuses)
        {
            StatusComboBox.Items.Add(status);
        }
    }

    private void InitializeFields()
    {
        NameTextBox.Text = _product.Name;
        PriceTextBox.Text = _product.Price.ToString();
        TypeTextBox.Text = _product.Characteristic?.ProductType;
        CategoryTextBox.Text = _product.Characteristic?.Category;
        DescriptionTextBox.Text = _product.Characteristic?.Description;
        ManufacturerTextBox.Text = _product.Characteristic?.Manufacturer;
        CountryTextBox.Text = _product.Characteristic?.Country;
        DatePicker.SelectedDate = TimeService.DateOnlyToDateTime(_product.Characteristic?.ManufactureDate);
        StatusComboBox.SelectedItem = _product.Characteristic?.ProductStatus;
    }

    public void OnProductAdded(EntityEventArgs e)
    {
        ProductAdded?.Invoke(this, e);
    }

    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        NameText.Foreground = new SolidColorBrush(Colors.Black);
        PriceTextBox.Foreground = new SolidColorBrush(Colors.Black);
        PriceTextBoxErorBlock.Text = "";
        StatusComboBoxErrorText.Text = "";
        ErrorTextBlock.Text = "";

        if (NameTextBox.Text == "")
        {
            NameText.Foreground = new SolidColorBrush(Colors.Red);
            return;
        }
        if (ErrorTextBlock.Text ==_product.Name && !_productService.IsProductNameUnique(NameTextBox.Text, AuthorizationService.AuthorizedUser!))
        {
            ErrorTextBlock.Text = "Такий продукт вже існує";
            return;
        }

        if (PriceTextBox.Text == "")
        {
            PriceText.Foreground = new SolidColorBrush(Colors.Red);
            return;
        }

        try
        {
            int result = Convert.ToInt32(PriceTextBox.Text);
        }
        catch (FormatException)
        {
            PriceTextBoxErorBlock.Text = "Невірний формат";
            return;
        }
        catch (OverflowException)
        {
            PriceTextBoxErorBlock.Text = "Невірний формат";
            return;
        }
        if (StatusComboBox.SelectedItem == null)
        {
            StatusComboBoxErrorText.Text = "Виберіть статус продукту";
            return;
        }
        _product.Name = NameTextBox.Text;
        _product.Price = Convert.ToInt32(PriceTextBox.Text);
        _product.Characteristic!.ProductType = TypeTextBox.Text;
        _product.Characteristic!.Category = CategoryTextBox.Text;
        _product.Characteristic!.Description = DescriptionTextBox.Text;
        _product.Characteristic!.Manufacturer = ManufacturerTextBox.Text;
        _product.Characteristic!.Country = CountryTextBox.Text;
        _product.Characteristic!.ManufactureDate = DateOnly.FromDateTime(DatePicker.SelectedDate ?? DateTime.Now);
        _product.Characteristic!.ProductStatus = (ProductStatus)StatusComboBox.SelectedItem!;

        Product? updated = _productService.Update(_product);



        if (updated is null)
        {
            ErrorTextBlock.Text = "Помилка при редагуванні продукту";
            Log.Information($"{nameof(UpdateProductWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Error while updating product");
        }
        else
        {
            ErrorTextBlock.Text = "Продукт успішно відредагований";
            Log.Information($"{nameof(UpdateProductWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Product {updated.Name} updated");
            OnProductAdded(new EntityEventArgs(_product));
            Close();
        }
    }
}