﻿using Presentation.Events;
using System;
using System.Linq;
using System.Windows;
using Business.Services;

namespace Presentation;

public partial class SearchWindow : Window
{
    public event EventHandler<ClientPredicateEventArgs> SearchApplied;

    public SearchWindow()
    {
        SearchApplied += (sender, args) => { };
        InitializeComponent();
    }

    private void SearchButtonClick(object sender, RoutedEventArgs e)
    {
        string[] words = SearchTextBox.Text.Split(' ');
        OnSearchApplied(new ClientPredicateEventArgs(c =>
            words.Any(w => ClientService.ClientContains(c, w))));
        Close();
    }

    protected virtual void OnSearchApplied(ClientPredicateEventArgs e)
    {
        SearchApplied?.Invoke(this, e);
    }
}