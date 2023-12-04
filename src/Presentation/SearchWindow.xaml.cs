using Presentation.Events;
using System;
using System.Linq;
using System.Windows;
using Business.Services;

namespace Presentation;

public partial class SearchWindow : Window
{
    public event EventHandler<EntityPredicateEventArgs> SearchApplied;

    public SearchWindow()
    {
        SearchApplied += (sender, args) => { };
        InitializeComponent();
    }

    private void SearchButtonClick(object sender, RoutedEventArgs e)
    {
        string[] words = SearchTextBox.Text.Split(' ');

        OnSearchApplied(new EntityPredicateEventArgs(
            c => words.Any(w => ClientService.ClientContains(c, w)),
            p => words.Any(w => ProductService.ProductContains(p, w))));

        Close();
    }

    protected virtual void OnSearchApplied(EntityPredicateEventArgs e)
    {
        SearchApplied?.Invoke(this, e);
    }
}