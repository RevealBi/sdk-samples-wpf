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
        }

        private void RevealView_VisualizationLinkingDashboard(object sender, VisualizationLinkingDashboardEventArgs e)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Dashboards/Campaigns.rdash");
            using (var stream = File.OpenRead(path))
            {
                e.Callback("Campaigns", stream);
            }
        }
    }
}
