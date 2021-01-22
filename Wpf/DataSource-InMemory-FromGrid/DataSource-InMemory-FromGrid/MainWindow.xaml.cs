using DataSource_InMemory.Business;
using DataSource_InMemory.Data;
using DataSource_InMemory.Providers;
using Reveal.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DataSource_InMemory_FromGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IEnumerable<Sale> _salesDataCache = SalesDataGenerator.GenerateSales(10000);
        bool _selectionInProgress = false;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            _grid.DataSource = _salesDataCache;
            _grid.SelectedItemsChanging += Grid_SelectedItemsChanging;
            _grid.PreviewMouseLeftButtonUp += Grid_PreviewMouseLeftButtonUp;

            RevealSdkSettings.DataProvider = new InMemoryDataProvider(_salesDataCache);

            _revealView.DataSourcesRequested += RevealView_DataSourcesRequested;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards/SalesWithGlobalFilter.rdash");
            _revealView.Dashboard = new RVDashboard(filePath);
        }

        private void RevealView_DataSourcesRequested(object sender, DataSourcesRequestedEventArgs e)
        {
            List<RVDashboardDataSource> datasources = new List<RVDashboardDataSource>();
            List<RVDataSourceItem> datasourceItems = new List<RVDataSourceItem>();

            var inMemoryDataSourceItem = new RVInMemoryDataSourceItem("SalesRecords")
            {
                Title = "Sales Records"
            };

            datasourceItems.Add(inMemoryDataSourceItem);
            e.Callback(new RevealDataSources(datasources, datasourceItems, true));
        }

        private void Grid_SelectedItemsChanging(object sender, Infragistics.Windows.DataPresenter.Events.SelectedItemsChangingEventArgs e)
        {
            _selectionInProgress = true;
        }

        private void Grid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_selectionInProgress)
            {
                _selectionInProgress = false;
                UpdateDashboardData();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _grid.SelectedDataItems = null;
            _grid.ActiveRecord = null;
            UpdateDashboardData();
        }

        void UpdateDashboardData()
        {
            var data = _grid.SelectedDataItems?.Cast<Sale>().AsEnumerable();
            if (data is null)
                data = _salesDataCache;

            ((InMemoryDataProvider)RevealSdkSettings.DataProvider).SalesRecords = data;
            _revealView.RefreshDashboardData();
        }
    }
}
