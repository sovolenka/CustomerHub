﻿using System.Windows;
using System.Windows.Media;
using Serilog;

namespace Presentation
{
    public partial class AnalysisWindow : Window
    {
        public AnalysisWindow()
        {
            InitializeComponent();
            Log.Information($"{nameof(AnalysisWindow)} opened");
        }

        private void ToActiveInactiveClient(object sender, RoutedEventArgs e)
        {
            newClients.Foreground = Brushes.Black;
            activeInActiveClients.Foreground = new SolidColorBrush(Color.FromArgb(117, 113, 113, 100));
            AnalysisPages.NavigationService.Navigate(null);
            AnalysisPages.Navigate(new ActiveInactiveClients());
        }

        private void ToNewClientsAnalysis(object sender, RoutedEventArgs e)
        {
            newClients.Foreground = new SolidColorBrush(Color.FromArgb(117, 113, 113, 100));
            activeInActiveClients.Foreground = Brushes.Black;
            AnalysisPages.NavigationService.Navigate(null);
            AnalysisPages.Navigate(new NewClientsAnalysis());
        }
    }
}