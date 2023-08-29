using Reveal.Sdk;
using Reveal.Sdk.Data;
using Reveal.Sdk.Data.Rest;
using System.Collections.Generic;
using System.Windows;

namespace DataSource_Rest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            _revealView.DataSourcesRequested += RevealView_DataSourcesRequested;
        }

        private void RevealView_DataSourcesRequested(object sender, DataSourcesRequestedEventArgs e)
        {
            var dataSources = new List<RVDashboardDataSource>();
            var items = new List<RVDataSourceItem>();

            var restDataSource = new RVRESTDataSource();
            restDataSource.Id = "RestDataSource";
            restDataSource.Url = "https://excel2json.io/api/share/6e0f06b3-72d3-4fec-7984-08da43f56bb9";
            restDataSource.Title = "Sales by Category";
            restDataSource.Subtitle = "Excel2Json";
            restDataSource.UseAnonymousAuthentication = true;
            dataSources.Add(restDataSource);

            e.Callback(new RevealDataSources(dataSources, items, false));
        }
    }
}
