using System;
using System.Windows;
using Business.Services;
using Data.Models;
using Presentation.Events;

namespace Presentation;

public partial class AddClientWindow : Window
{
    private readonly ClientService _clientService;
    private readonly TimeService _timeService;
    
    // Event to notify that a new client is added
    public event EventHandler<ClientEventArgs> ClientAdded;

    public AddClientWindow()
    {
        _clientService = new ClientService();
        _timeService = new TimeService();
        ClientAdded += (sender, args) => { };
        InitializeComponent();
        ClearField();
    }

    private void ClearField()
    {
        FirstNameTextBox.Text = "";
        SecondNameTextBox.Text = "";
        ThirdNameTextBox.Text = "";
        PhoneNumberTextBox.Text = "";
        EmailTextBox.Text = "";
        AddressTextBox.Text = "";
        FactoryTextBox.Text = "";
    }

    private void AddButtonClick(object sender, RoutedEventArgs e)
    {
        // TODO: Add validation

        Client client = new Client(
            FirstNameTextBox.Text,
            SecondNameTextBox.Text,
            ThirdNameTextBox.Text,
            PhoneNumberTextBox.Text,
            EmailTextBox.Text,
            AddressTextBox.Text,
            FactoryTextBox.Text,
            DateOnly.FromDateTime(_timeService.GetCurrentDateTime()),
            ClientStatus.Active
        );

        Client? added = _clientService.Add(client, AuthorizationService.AuthorizedUser!);
        if (added is null)
        {
            ErrorTextBlock.Text = "Помилка при додаванні клієнта";
        }
        else
        {
            // Notify subscribers (e.g., the ClientListWindow) that a new client is added
            OnClientAdded(new ClientEventArgs(added));
            ErrorTextBlock.Text = "Клієнт успішно доданий";
            ClearField();
        }
    }

    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    // Method to raise the event
    protected virtual void OnClientAdded(ClientEventArgs e)
    {
        ClientAdded?.Invoke(this, e);
    }
}