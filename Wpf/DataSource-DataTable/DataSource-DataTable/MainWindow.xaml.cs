using DataSource_DataTable.Providers;
using Reveal.Sdk;
using System.Collections.Generic;
using System.Windows;

namespace DataSource_DataTable
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            RevealSdkSettings.DataProvider = new DataTableDataProvider();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _revealView.DataSourcesRequested += RevealView_DataSourcesRequested;
        }

        private void RevealView_DataSourcesRequested(object sender, DataSourcesRequestedEventArgs e)
        {
            List<RVDashboardDataSource> datasources = new List<RVDashboardDataSource>();
            List<RVDataSourceItem> datasourceItems = new List<RVDataSourceItem>();

            var inMemoryDataSourceItem = new RVInMemoryDataSourceItem("employees") 
            { 
                Title = "Employees Data Table", 
                Id = "Employees Data Table" 
            };

            datasourceItems.Add(inMemoryDataSourceItem);

            e.Callback(new RevealDataSources(datasources, datasourceItems, true));
        }
    }
}
