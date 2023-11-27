using Presentation.Events;
using System;
using System.Linq;
using System.Windows;
using Business.Services;

namespace Presentation;

public partial class SearchWindow : Window
{
    public event EventHandler<EventArgs> SearchApplied;

    public SearchWindow()
    {
        SearchApplied += (sender, args) => { };
        InitializeComponent();
    }

    private void SearchButtonClick(object sender, RoutedEventArgs e)
    {
        string[] words = SearchTextBox.Text.Split(' ');
        if (sender is ClientListWindow)
        {
            OnSearchApplied(new ClientPredicateEventArgs(c =>
                words.Any(w => ClientService.ClientContains(c, w))));
        }
        else if (sender is ProductListWindow)
        {
            OnSearchApplied(new ProductPredicateEventArgs(p =>
                words.Any(w => ProductService.ProductContains(p, w))));
        }
       
        Close();
    }

    protected virtual void OnSearchApplied(EventArgs e)
    {
        SearchApplied?.Invoke(this, e);
    }
}