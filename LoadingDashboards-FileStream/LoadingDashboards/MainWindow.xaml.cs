using Reveal.Sdk;
using System;
using System.IO;
using System.Windows;

namespace LoadingDashboards
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
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards/Sales.rdash");
            using (var stream = File.OpenRead(filePath))
            {
                _revealView.Dashboard = new RVDashboard(stream);
            }
        }
    }
}
