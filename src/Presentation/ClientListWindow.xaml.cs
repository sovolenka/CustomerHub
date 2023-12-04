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
            ClientList.ItemsSource = _clientService.GetAllByUser(AuthorizationService.AuthorizedUser!);
        }

    private void OpenAddClientWindow(object sender, RoutedEventArgs e)
    {
        AddClientWindow addClientWindow = new();
        addClientWindow.ClientAdded += OnClientsUpdate;
        addClientWindow.Show();
    }

        // Event handler for the ClientAdded event
        private void OnClientsUpdate(object? sender, EntityEventArgs e)
        {
            ClientList.ItemsSource = _clientService.GetAllByUser(AuthorizationService.AuthorizedUser!);
        }

        private void OnPredicateUpdate(object? sender, EntityPredicateEventArgs e)
        {
            Predicate<Client> predicate = e.ClientPredicate;

            ClientList.ItemsSource = _clientService.GetAllByUser(AuthorizationService.AuthorizedUser!)
                .Where(client => predicate(client));
        }

    private void OpenEditClientWindow(object sender, RoutedEventArgs e)
    {
        if (ClientList.SelectedItem is null)
        {
            MessageBox.Show("Виберіть клієнта для редагування");
            return;
        }

        Client selectedClient = (Client)ClientList.SelectedItem;
        UpdateClientWindow updateClientWindow = new (selectedClient);
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
            ClientList.ItemsSource = _clientService.GetAllByUser(AuthorizationService.AuthorizedUser!);
        }

    private void SearchClientClick(object sender, RoutedEventArgs e)
    {
        SearchWindow searchWindow = new();
        searchWindow.SearchApplied += OnPredicateUpdate;
        searchWindow.Show();
    }

        private void AllClientsClick(object sender, RoutedEventArgs e)
        {
            ClientList.ItemsSource = _clientService.GetAllByUser(AuthorizationService.AuthorizedUser!);
        }

    private void AnalysisClientClick(object sender, RoutedEventArgs e)
    {
        AnalysisWindow analysisWindow = new();
        analysisWindow.Show();
    }
}