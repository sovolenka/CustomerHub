using Data.Models;
using Presentation.Events;
using System;
using System.Linq;
using System.Windows;

namespace Presentation;

/// <summary>
/// Interaction logic for ClientSearchWindow.xaml
/// </summary>
public partial class ClientSearchWindow : Window
{
    public event EventHandler<ClientPredicateEventArgs> SearchApplied;

    public ClientSearchWindow()
    {
        SearchApplied += (sender, args) => { };
        InitializeComponent();
    }

    public void SearchButtonClick(object sender, RoutedEventArgs e)
    {
        string[] words = SearchTextBox.Text.Split(' ');
        OnSearchApplied(new ClientPredicateEventArgs(
            c => c.GetType().GetFields().Where(f => words.Any(w => w == f.ToString())).Any()));
        Close();
    }

    protected virtual void OnSearchApplied(ClientPredicateEventArgs e)
    {
        SearchApplied?.Invoke(this, e);
    }
}
