using Data.Models;
using Presentation.Events;
using System;
using System.Windows;
using Business.Services;

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
        StatusComboBox.SelectedItem = _product.Characteristic?.Status;
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
        _product.Name = NameTextBox.Text;
        _product.Price = Convert.ToInt32(PriceTextBox.Text);
        _product.Characteristic!.ProductType = TypeTextBox.Text;
        _product.Characteristic!.Category = CategoryTextBox.Text;
        _product.Characteristic!.Description = DescriptionTextBox.Text;
        _product.Characteristic!.Manufacturer = ManufacturerTextBox.Text;
        _product.Characteristic!.Country = CountryTextBox.Text;
        _product.Characteristic!.ManufactureDate = DateOnly.FromDateTime(DatePicker.SelectedDate ?? DateTime.Now);
        _product.Characteristic!.Status = (ProductStatus)StatusComboBox.SelectedItem!;

        Product? updated = _productService.Update(_product);

        if (updated is null)
        {
            ErrorTextBlock.Text = "Помилка при редагуванні продукту";
        }
        else
        {
            ErrorTextBlock.Text = "Продукт успішно відредагований";
            OnProductAdded(new EntityEventArgs(_product));
            Close();
        }
    }
}