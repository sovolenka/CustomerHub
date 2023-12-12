using System.Windows.Controls;
using Business.Services;
using LiveCharts.Wpf;
using LiveCharts;
using Serilog;

namespace Presentation
{
    public partial class ActiveInactiveClients : Page
    {
        private readonly ClientService _clientService;

        public SeriesCollection PieSeriesCollection { get; }
        
        public ActiveInactiveClients()
        {
            this._clientService = new ClientService();
            this.PieSeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Активний",
                    Values = new ChartValues<int>
                        { this._clientService.GetActiveClientsCount(AuthorizationService.AuthorizedUser!) },
                    DataLabels = true,
                },
                new PieSeries 
                {
                    Title = "Неактивний",
                    Values = new ChartValues<int>
                        { _clientService.GetInactiveClientsCount(AuthorizationService.AuthorizedUser!) },
                    DataLabels = true,
                }
            };

            this.DataContext = this;

            if (AuthorizationService.AuthorizedUser != null)
            {
                Log.Information(
                    $"{nameof(ActiveInactiveClients)}. {AuthorizationService.AuthorizedUser.Email}. Page opened");
            }
        }
    }
}
