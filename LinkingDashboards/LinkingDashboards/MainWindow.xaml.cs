using Reveal.Sdk;
using System;
using System.IO;
using System.Windows;

namespace LinkingDashboards
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards/Marketing.rdash");
            _revealView.Dashboard = new RVDashboard(filePath);

            _revealView.LinkedDashboardProvider = (dashboardId, title) => {
                var linkedFilePath = Path.Combine(Environment.CurrentDirectory, $"Dashboards/{dashboardId}.rdash");
                return new RVDashboard(linkedFilePath);
            };
        }
    }    
}
