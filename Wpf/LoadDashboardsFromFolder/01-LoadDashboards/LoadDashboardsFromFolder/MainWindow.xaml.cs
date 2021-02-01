using LoadDashboardsFromFolder.Business;
using System;
using System.IO;
using System.Windows;

namespace LoadDashboardsFromFolder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDashboardsFromFolder();
        }

        private void LoadDashboardsFromFolder()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Dashboards");
            var dashboards = Directory.GetFiles(path);

            foreach (var dashboard in dashboards)
            {
                var dashboardItem = new DashboardItem()
                {
                    Name = Path.GetFileNameWithoutExtension(dashboard),
                    Path = dashboard
                };
                _dashboardList.Items.Add(dashboardItem);
            }

            _dashboardList.SelectedIndex = 0;
        }

        private void DashboardList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedDashboardItem = (DashboardItem)e.AddedItems[0];
            _revealView.Dashboard = new Reveal.Sdk.RVDashboard(selectedDashboardItem.Path);
        }
    }
}
