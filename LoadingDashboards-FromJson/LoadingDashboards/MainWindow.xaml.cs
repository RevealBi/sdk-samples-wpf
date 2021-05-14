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

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards/Sales.json");
            var json = File.ReadAllText(filePath);
            _revealView.Dashboard = await RVDashboard.LoadFromJsonAsync(json);
        }
    }
}
