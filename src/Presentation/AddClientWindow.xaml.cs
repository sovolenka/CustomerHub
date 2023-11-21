using System;
using System.Windows;
using Business.Services;
using Data.Models;

namespace Presentation;

public partial class AddClientWindow : Window
{
    private readonly ClientService _clientService;
    private readonly TimeService _timeService;

    public AddClientWindow()
    {
        _clientService = new ClientService();
        _timeService = new TimeService();
        InitializeComponent();
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

        _clientService.Add(client, AuthorizationService.AuthorizedUser!);
        ErrorTextBlock.Text = "Христина успішно додана!";
        ClearField();
    }

    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}