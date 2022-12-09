using Reveal.Sdk;
using System.Collections.Generic;
using System.Windows;

namespace DataSource_SqlServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            RevealSdkSettings.AuthenticationProvider = new AuthenticationProvider();

            InitializeComponent();
            _revealView.DataSourcesRequested += RevealView_DataSourcesRequested;
        }

        private void RevealView_DataSourcesRequested(object sender, Reveal.Sdk.DataSourcesRequestedEventArgs e)
        {
            var dataSources = new List<RVDashboardDataSource>();
            var items = new List<RVDataSourceItem>();

            var sqlDataSource = new RVSqlServerDataSource()
            {
                Host = "your-db-host",
                Database = "your-db-name",
                Port = 1234,
                Title = "My SQL Server",
            };
            dataSources.Add(sqlDataSource);

            e.Callback(new RevealDataSources(dataSources, null, false));
        }
    }
}
