using LoadDashboardsFromFolder.Business;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

        private void DashboardList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedDashboardItem = (DashboardItem)e.AddedItems[0];
            _revealView.Dashboard = new Reveal.Sdk.RVDashboard(selectedDashboardItem.Path);
        }

        private async void RevealView_SaveDashboard(object sender, Reveal.Sdk.DashboardSaveEventArgs e)
        {
            var dashboardItem = _dashboardItems.Where(x => x.Name == e.Name).FirstOrDefault();

            //if the dashboard name is null or the Title of the dashboard has changed let's prompt to SaveAs
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
                }
            }
            else
            {
                var data = await e.Serialize();

                using (var output = File.Open(dashboardItem.Path, FileMode.Create))
                {
                    output.Write(data, 0, data.Length);
                }
            }

            e.SaveFinished();
        }
    }
}
