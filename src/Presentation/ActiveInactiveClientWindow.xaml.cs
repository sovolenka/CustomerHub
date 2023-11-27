using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using Data.Models;
using Business.Services;
using Data.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Presentation.Events;

namespace Presentation;

public partial class ActiveInactiveClientWindow : Window
{
    public SeriesCollection PieSeriesCollection { get; set; }

    private readonly ClientService _clientService;

    public ActiveInactiveClientWindow()
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
