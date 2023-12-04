using System.Windows.Controls;
using Business.Services;
using LiveCharts.Wpf;
using LiveCharts;


namespace Presentation
{

    public partial class ActiveInactiveClients : Page
    {
        public SeriesCollection PieSeriesCollection { get; set; }

        private readonly ClientService _clientService;

        public ActiveInactiveClients()
        {
            _clientService = new ClientService();
            InitializeComponent();

            PieSeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Активний",
                    Values = new ChartValues<int> { _clientService.GetActiveClientsCount(AuthorizationService.AuthorizedUser!) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Неактивний",
                    Values = new ChartValues<int> { _clientService.GetInactiveClientsCount(AuthorizationService.AuthorizedUser!) },
                    DataLabels = true
                }
            };

            DataContext = this;
        }
    }
}
