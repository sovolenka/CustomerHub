﻿using System.Windows;
using System.Windows.Input;
using Business.Services;
using Serilog;

namespace Presentation;

public partial class ForgotPasswordWindow : Window
{
    public ForgotPasswordWindow()
    {
        InitializeComponent();
        Log.Information($"{nameof(ForgotPasswordWindow)}. {AuthorizationService.AuthorizedUser?.Email}. Window opened");
    }

    private void ImageMouseDown(object sender, MouseButtonEventArgs e)
    {
        AuthorizationWindow authorizationWindow = new AuthorizationWindow();
        Hide();
        authorizationWindow.ShowDialog();
        Close();
        Log.Information($"{nameof(ForgotPasswordWindow)}. {AuthorizationService.AuthorizedUser?.Email}. AuthorizationWindow opened");
    }

    private void SendEmailButton(object sender, MouseButtonEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}