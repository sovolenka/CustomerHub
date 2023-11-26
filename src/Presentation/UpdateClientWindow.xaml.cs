using System;
using System.Windows;
using Business.Services;
using Data.Models;
using Presentation.Events;


namespace Presentation;

public partial class UpdateClientWindow : Window
{
    private Client _client;
    private readonly ClientService _clientService;
    private readonly TimeService _timeService;
    
    // Event to notify that a new client is added
    public event EventHandler<ClientEventArgs> ClientAdded;
    
    public UpdateClientWindow(Client client)
    {
        _clientService = new ClientService();
        _timeService = new TimeService();
        ClientAdded += (sender, args) => { };
        InitializeComponent();
        InitializeStatusComboBox();
        _client = client;
        InitializeClient(client);
    }

    private void InitializeStatusComboBox()
    {
        var statuses = Enum.GetValues(typeof(ClientStatus));
        foreach (var status in statuses)
        {
            StatusComboBox.Items.Add(status);
        }
    }
    
    private void InitializeClient(Client client)
    {
        FirstNameTextBox.Text = client.FirstName;
        SecondNameTextBox.Text = client.SecondName;
        ThirdNameTextBox.Text = client.ThirdName;
        EmailTextBox.Text = client.Email;
        PhoneNumberTextBox.Text = client.PhoneNumber;
        AddressTextBox.Text = client.Address;
        FactoryTextBox.Text = client.Factory;
        StatusComboBox.SelectedItem = client.Status;
    }

    private void EditButtonClick(object sender, RoutedEventArgs e)
    {
        // TODO: Add validation
        
        _client.FirstName = FirstNameTextBox.Text;
        _client.SecondName = SecondNameTextBox.Text;
        _client.ThirdName = ThirdNameTextBox.Text;
        _client.Email = EmailTextBox.Text;
        _client.PhoneNumber = PhoneNumberTextBox.Text;
        _client.Address = AddressTextBox.Text;
        _client.Factory = FactoryTextBox.Text;
        _client.Status = (ClientStatus) StatusComboBox.SelectedItem!;
        
        Client? updated = _clientService.Update(_client);
        
        if (updated is null)
        {
            ErrorTextBlock.Text = "Помилка при редагуванні клієнта";
        }
        else
        {
            ErrorTextBlock.Text = "Клієнт успішно відредагований";
            OnClientAdded(new ClientEventArgs(_client));
            Close();
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