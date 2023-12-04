using System.Windows;

namespace Presentation
{
    public partial class AnalysisWindow : Window
    {

        public AnalysisWindow()
        {
            InitializeComponent();
        }
        private void ToActiveInactiveClient(object sender, RoutedEventArgs e)
        {
            AnalysisPages.Navigate(new ActiveInactiveClients());
        }
        private void ToNewClientsAnalysis(object sender, RoutedEventArgs e)
        {
            AnalysisPages.Navigate(new NewClientsAnalysis());
        }
    }
}
