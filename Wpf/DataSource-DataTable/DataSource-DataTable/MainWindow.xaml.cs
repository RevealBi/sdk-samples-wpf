using DataSource_DataTable.Providers;
using Infragistics.Sdk;
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
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _revealView.Settings = new RevealSettings(null);

            _revealView.DataSourcesRequested += RevealView_DataSourcesRequested;
            _revealView.DataProvider = new DataTableDataProvider();
        }

        private void RevealView_DataSourcesRequested(object sender, DataSourcesRequestedEventArgs e)
        {
            List<object> datasources = new List<object>();
            List<object> datasourceItems = new List<object>();

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
