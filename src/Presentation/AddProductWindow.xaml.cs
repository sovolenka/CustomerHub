﻿using System;
using System.Windows;
using System.Windows.Media;
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

        if (!_productService.IsProductNameUnique(NameTextBox.Text, AuthorizationService.AuthorizedUser!))
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

        Characteristic characteristic = new(
            TypeTextBox.Text,
            CategoryTextBox.Text,
            DescriptionTextBox.Text,
            ManufacturerTextBox.Text,
            CountryTextBox.Text,
            DateOnly.FromDateTime(DateTime.Now),
            (ProductStatus)StatusComboBox.SelectedItem!
        );
        Product product = new(
            NameTextBox.Text,
            Convert.ToInt32(PriceTextBox.Text),
            characteristic
        );

        Product? added = _productService.Add(product, AuthorizationService.AuthorizedUser!);
        if (added is null)
        {
            ErrorTextBlock.Text = "Помилка при додаванні продукту";
            Log.Error(
                $"{nameof(AddProductWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Error while adding product: {product.Name}");
        }
        else
        {
            OnProductAdded(new EntityEventArgs(added));
            ErrorTextBlock.Text = "Продукт успішно додано";
            Log.Information(
                $"{nameof(AddProductWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Product added: {product.Name}");
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