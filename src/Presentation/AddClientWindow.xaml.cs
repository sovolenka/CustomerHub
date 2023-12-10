using System;
using System.Windows;
using System.Windows.Media;
using Business.Services;
using Data.Models;
using Presentation.Events;
using Business.Validators;
using Business.Validators.Exceptions;

namespace Presentation;

public partial class AddClientWindow : Window
{
    private readonly ClientService _clientService;
    private readonly TimeService _timeService;
    private readonly AuthorizationService _authorizationService;
    private readonly UserService _userService;
    
    // Event to notify that a new client is added
    public event EventHandler<EntityEventArgs> ClientAdded;

    public AddClientWindow()
    {
        _clientService = new ClientService();
        _timeService = new TimeService();
        _authorizationService = new AuthorizationService();
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

        if (!_clientService.IsEmailUnique(EmailTextBox.Text, AuthorizationService.AuthorizedUser))
        {
            EmailErrorTextBlock.Text = "Пошта вже використовується";
            return;
        }

        if (!_clientService.IsPhoneNumberUnique(PhoneNumberTextBox.Text, AuthorizationService.AuthorizedUser))
        {
            PhoneNumberErrorTextBlock.Text = "Номер вже використовується";
            return;
        }

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
            OnClientAdded(new EntityEventArgs(added));
            ErrorTextBlock.Text = "Клієнт успішно доданий";
            ClearField();
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