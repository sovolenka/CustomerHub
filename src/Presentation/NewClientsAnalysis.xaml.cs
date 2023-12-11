using Business.Services;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    public partial class NewClientsAnalysis : Page, INotifyPropertyChanged
    {
        private readonly ClientService _clientService;
        private DateTime  _startDate;
        private DateTime  _endDate;
        private SeriesCollection _columnSeriesCollection;
        private string[] _labels;
        public event PropertyChangedEventHandler? PropertyChanged;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                    UpdateChart();
                }
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                    UpdateChart();
                }
            }
        }

        public SeriesCollection ColumnSeriesCollection
        {
            get { return _columnSeriesCollection; }
            set
            {
                if (_columnSeriesCollection != value)
                {
                    _columnSeriesCollection = value;
                    OnPropertyChanged(nameof(ColumnSeriesCollection));
                }
            }
        }

        public string[] Labels
        {
            get { return _labels; }
            set
            {
                if (_labels != value)
                {
                    _labels = value;
                    OnPropertyChanged(nameof(Labels));
                }
            }
        }

        

        public NewClientsAnalysis()
        {
            InitializeComponent();
            _clientService = new ClientService();
            EndDate = DateTime.Today;
            StartDate = DateTime.Today.AddMonths(-1);
            UpdateChart();
            DataContext = this;
        }

        private void UpdateChart()
        {
            var clientsByDay = _clientService.GetClientsCountByDay(
                AuthorizationService.AuthorizedUser!,
                DateOnly.FromDateTime(StartDate),
                DateOnly.FromDateTime(EndDate)
            );

            ColumnSeriesCollection = new SeriesCollection();
            Labels = clientsByDay?.Select(entry => entry.Key.ToShortDateString()).ToArray() ?? Array.Empty<string>();

            ColumnSeriesCollection.Add(new ColumnSeries
            {
                Title = "Кількість клієнтів",
                Values = new ChartValues<int>(clientsByDay?.Select(entry => entry.Value) ?? Enumerable.Empty<int>())
            });

        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
