using System;
using System.Windows;
using System.Windows.Media;
using Business.Services;
using Data.Models;
using Presentation.Events;
using Business.Validators;
using Business.Validators.Exceptions;
using Serilog;

namespace Presentation;

public partial class AddClientWindow : Window
{
    private readonly ClientService _clientService;
    private readonly TimeService _timeService;
    
    // Event to notify that a new client is added
    public event EventHandler<EntityEventArgs> ClientAdded;

    public AddClientWindow()
    {
        _clientService = new ClientService();
        _timeService = new TimeService();
        ClientAdded += (sender, args) => { };
        InitializeComponent();
        ClearField();
        Log.Information($"{nameof(AddClientWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Window opened");
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
        EmailErrorTextBlock.Text = "";
        PhoneNumberErrorTextBlock.Text = "";
        FirstNameText.Foreground = new SolidColorBrush(Colors.Black);
        FirstNameText.Foreground = new SolidColorBrush(Colors.Black);
        EmailTextBox.Foreground = new SolidColorBrush(Colors.Black);
        PhoneNumberTextBox.Foreground = new SolidColorBrush(Colors.Black);
    }

    private void AddButtonClick(object sender, RoutedEventArgs e)
    {
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
                Log.Information($"{nameof(AddClientWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Invalid email format");
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
                Log.Information($"{nameof(AddClientWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Invalid phone number format: {PhoneNumberTextBox.Text}");
                return;
            }
        }


        if (!_clientService.IsEmailUnique(EmailTextBox.Text, AuthorizationService.AuthorizedUser))
        {
            EmailErrorTextBlock.Text = "Пошта вже використовується";
            Log.Information($"{nameof(AddClientWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Email {EmailTextBox.Text} already exists");
            return;
        }

        if (!_clientService.IsPhoneNumberUnique(PhoneNumberTextBox.Text, AuthorizationService.AuthorizedUser))
        {
            PhoneNumberErrorTextBlock.Text = "Номер вже використовується";
            Log.Information($"{nameof(AddClientWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Phone number {PhoneNumberTextBox.Text} already exists");
            return;
        }

        Client client = new (
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
            Log.Information($"{nameof(AddClientWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Error while adding client {client.Email}");
        }
        else
        {
            // Notify subscribers (e.g., the ClientListWindow) that a new client is added
            OnClientAdded(new EntityEventArgs(added));
            ErrorTextBlock.Text = "Клієнт успішно доданий";
            Log.Information($"{nameof(AddClientWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Client {client.Email} added successfully");
            ClearField();
        }
    }

    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
        Log.Information($"{nameof(AddClientWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Window closed");
    }

    // Method to raise the event
    protected virtual void OnClientAdded(EntityEventArgs e)
    {
        ClientAdded?.Invoke(this, e);
    }
}