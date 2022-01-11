using LoadDashboardsFromFolder.Business;
using Microsoft.Win32;
using Reveal.Sdk;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows;

namespace LoadDashboardsFromFolder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string _defaultDirctory = Path.Combine(Environment.CurrentDirectory, "Dashboards");

        ObservableCollection<DashboardItem> _dashboardItems = new ObservableCollection<DashboardItem>();

        public MainWindow()
        {
            InitializeComponent();

            //This must be set in order to properly find the local excel file on disk
            RevealSdkSettings.LocalDataFilesRootFolder = Path.Combine(Environment.CurrentDirectory, "Data");

            _revealView.DataSourcesRequested += RevealView_DataSourcesRequested;
            _revealView.SaveDashboard += RevealView_SaveDashboard;

            LoadDashboardsFromFolder();
        }

        private void LoadDashboardsFromFolder()
        {
            foreach (var dashboard in Directory.GetFiles(_defaultDirctory))
            {
                var dashboardItem = new DashboardItem()
                {
                    Name = Path.GetFileNameWithoutExtension(dashboard),
                    Path = dashboard
                };
                _dashboardItems.Add(dashboardItem);
            }

            _dashboardList.ItemsSource = _dashboardItems;
            _dashboardList.SelectedIndex = 0;
        }

        private void AddNewDashboard_Click(object sender, RoutedEventArgs e)
        {
            _revealView.Dashboard = new RVDashboard();
        }

        private void DashboardList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedDashboardItem = (DashboardItem)e.AddedItems[0];
            _revealView.Dashboard = new Reveal.Sdk.RVDashboard(selectedDashboardItem.Path);
        }

        private async void RevealView_SaveDashboard(object sender, Reveal.Sdk.DashboardSaveEventArgs e)
        {
            var dashboardItem = _dashboardItems.Where(x => x.Name == e.Name).FirstOrDefault();

            //if the dashboard name is null we couldn't find it in our existing dashboard list
            //if the Title of the dashboard has changed the e.Name won't match an existing dashboard, let's prompt to SaveAs
            if (dashboardItem is null || e.IsSaveAs)
            {
                var saveDialog = new SaveFileDialog()
                {
                    DefaultExt = ".rdash",
                    FileName = e.Name + ".rdash",
                    Filter = "Reveal Dashboard (*.rdash)|*.rdash",
                    InitialDirectory = _defaultDirctory
                };

                if (saveDialog.ShowDialog() == true)
                {
                    var newDashboard = new DashboardItem()
                    {
                        Name = Path.GetFileNameWithoutExtension(saveDialog.FileName),
                        Path = saveDialog.FileName
                    };

                    using (var stream = new FileStream(newDashboard.Path, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        var data = await e.SerializeWithNewName(newDashboard.Name);
                        await stream.WriteAsync(data, 0, data.Length);
                    }

                    _dashboardItems.Add(newDashboard);
                    _dashboardList.SelectedItem = newDashboard;

                    e.SaveFinished();
                }
            }
            else
            {
                var data = await e.Serialize();
                using (var output = File.Open(dashboardItem.Path, FileMode.Create))
                {
                    output.Write(data, 0, data.Length);
                }

                e.SaveFinished();
            }
        }

        private void RevealView_DataSourcesRequested(object sender, Reveal.Sdk.DataSourcesRequestedEventArgs e)
        {
            var dataSources = new List<RVDashboardDataSource>();
            var items = new List<RVDataSourceItem>();

            var excelDataSourceItem = CreateExcelFileDataSource("Local Excel File", "Samples.xlsx");
            items.Add(excelDataSourceItem);

            var test = new RVExcelDataSource();

            e.Callback(new RevealDataSources(dataSources, items, true));
        }

        public static RVExcelDataSourceItem CreateExcelFileDataSource(string title, string path)
        {
            var localFileItem = new RVLocalFileDataSourceItem();
            localFileItem.Uri = $"local:/{path}"; // "local:/" is a required prefix

            var excelDataSourceItem = new RVExcelDataSourceItem(localFileItem);
            excelDataSourceItem.Title = title;

            return excelDataSourceItem;
        }
    }
}
