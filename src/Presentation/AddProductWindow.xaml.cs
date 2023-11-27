using System;
using System.Windows;
using Business.Services;
using Presentation.Events;

namespace Presentation;

public partial class AddProductWindow : Window
{
    private readonly ProductService _productService;

    // Event to notify that a new client is added
    public event EventHandler<ProductEventArgs> ProductAdded;
    public AddProductWindow()
    {
        _productService = new ProductService();
        ProductAdded += (sender, args) => { };
        InitializeComponent();
    }
}
