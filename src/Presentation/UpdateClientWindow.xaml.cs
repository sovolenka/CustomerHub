using System;
using System.Windows;
using System.Windows.Media;
using Business.Services;
using Business.Validators.Exceptions;
using Business.Validators;
using Data.Models;
using Presentation.Events;


namespace Presentation;

public partial class UpdateClientWindow : Window
{
    private Client _client;
    private readonly ClientService _clientService;
    private readonly TimeService _timeService;
    
    // Event to notify that a new client is added
    public event EventHandler<EntityEventArgs> ClientAdded;
    
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
        EmailErrorTextBlock.Text = "";
        PhoneNumberErrorTextBlock.Text = "";
        FirstNameText.Foreground = new SolidColorBrush(Colors.Black);
        FirstNameText.Foreground = new SolidColorBrush(Colors.Black);
        EmailTextBox.Foreground = new SolidColorBrush(Colors.Black);
        PhoneNumberTextBox.Foreground = new SolidColorBrush(Colors.Black);
        if (FirstNameTextBox.Text == "")
        {
            FirstNameText.Foreground = new SolidColorBrush(Colors.Red);
            return;
        }

        if (EmailTextBox.Text == "")
        {
            EmailText.Foreground = new SolidColorBrush(Colors.Red);
            return;
        }
        if (PhoneNumberTextBox.Text == "")
        {
            PhoneNumberText.Foreground = new SolidColorBrush(Colors.Red);
            return;
        }

        if (EmailTextBox.Text != "")
        {
            try
            {
                EmailValidator.Validate(EmailTextBox.Text);

            }
            catch (InvalidEmailException)
            {
                EmailErrorTextBlock.Text = "Неправильний формат електронної пошти";
                return;
            }
        }
        if (PhoneNumberTextBox.Text != "")
        {
            try
            {
                PhoneNumberValidator.Validate(PhoneNumberTextBox.Text);
            }
            catch (InvalidPhoneNumberException)
            {
                PhoneNumberErrorTextBlock.Text = "Неправильний формат номеру телефону";
                return;
            }
        }

        if (EmailTextBox.Text != _client.Email && !_clientService.IsEmailUnique(EmailTextBox.Text, AuthorizationService.AuthorizedUser))
        {
            EmailErrorTextBlock.Text = "Пошта вже використовується";
            return;
        }

        if (PhoneNumberTextBox.Text != _client.PhoneNumber && !_clientService.IsPhoneNumberUnique(PhoneNumberTextBox.Text, AuthorizationService.AuthorizedUser))
        {
            PhoneNumberErrorTextBlock.Text = "Номер вже використовується";
            return;
        }

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
            OnClientAdded(new EntityEventArgs(_client));
            Close();
        }
    }

    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
    
    // Method to raise the event
    protected virtual void OnClientAdded(EntityEventArgs e)
    {
        ClientAdded?.Invoke(this, e);
    }
}