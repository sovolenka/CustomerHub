using Data.Models;
using Presentation.Events;
using System;
using System.Windows;

namespace Presentation;

/// <summary>
/// Interaction logic for UpdateProductWindow.xaml
/// </summary>
public partial class UpdateProductWindow : Window
{
    public event EventHandler<ProductEventArgs> ProductAdded;

    public UpdateProductWindow(Product product)
    {
        ProductAdded += (sender, args) => { };
        InitializeComponent();
    }

    public void OnProductAdded(ProductEventArgs e)
    {
        ProductAdded?.Invoke(this, e);
    }
}
