using System;
using System.Linq;
using System.Windows;
using Business.Services;
using Data.Models;
using Presentation.Events;


namespace Presentation;

public partial class ClientListWindow : Window
{
    private readonly ClientService _clientService;

    public ClientListWindow()
    {
        _clientService = new ClientService();
        InitializeComponent();
        ClientList.ItemsSource = _clientService.GetAll(AuthorizationService.AuthorizedUser!);
    }

    private void OpenAddClientWindow(object sender, RoutedEventArgs e)
    {
        AddClientWindow addClientWindow = new AddClientWindow();
        addClientWindow.ClientAdded += OnClientsUpdate;
        addClientWindow.Show();
    }

    // Event handler for the ClientAdded event
    private void OnClientsUpdate(object? sender, ClientEventArgs e)
    {
        ClientList.ItemsSource = _clientService.GetAll(AuthorizationService.AuthorizedUser!);
    }

    private void OnPredicateUpdate(object? sender, ClientPredicateEventArgs e)
    {
        if (e.Predicate is null)
        {
            ClientList.ItemsSource = _clientService.GetAll(AuthorizationService.AuthorizedUser!);
        }
        else
        {
            ClientList.ItemsSource = _clientService.GetAll(AuthorizationService.AuthorizedUser!)
                .Where(client => e.Predicate(client));
        }
    }

    private void OpenEditClientWindow(object sender, RoutedEventArgs e)
    {
        if (ClientList.SelectedItem is null)
        {
            MessageBox.Show("Виберіть клієнта для редагування");
            return;
        }

        Client selectedClient = (Client)ClientList.SelectedItem;
        UpdateClientWindow updateClientWindow = new UpdateClientWindow(selectedClient);
        updateClientWindow.ClientAdded += OnClientsUpdate;
        updateClientWindow.Show();
    }

    private void DeleteClientClick(object sender, RoutedEventArgs e)
    {
        if (ClientList.SelectedItem is null)
        {
            MessageBox.Show("Виберіть клієнта для видалення");
            return;
        }

        Client selectedClient = (Client)ClientList.SelectedItem;
        MessageBoxResult messageBoxResult = MessageBox.Show(
            $"Ви впевнені, що хочете видалити {selectedClient.FirstName} {selectedClient.SecondName}?",
            "Delete Confirmation", MessageBoxButton.YesNo);
        if (messageBoxResult == MessageBoxResult.No) return;
        _clientService.Remove(selectedClient);
        ClientList.ItemsSource = _clientService.GetAll(AuthorizationService.AuthorizedUser!);
    }

    private void SearchClientClick(object sender, RoutedEventArgs e)
    {
        SearchWindow searchWindow = new();
        searchWindow.SearchApplied += OnPredicateUpdate;
        searchWindow.Show();
    }

    private void AllClientsClick(object sender, RoutedEventArgs e)
    {
        ClientList.ItemsSource = _clientService.GetAll(AuthorizationService.AuthorizedUser!);
    }

    private void AnalysisClientClick(object sender, RoutedEventArgs e)
    {
    }

    private void ActiveInactiveClientWindow(object sender, RoutedEventArgs e)
    {
        ActiveInactiveClientWindow activeInactiveClientWindow = new();
        activeInactiveClientWindow.Show();
    }
}