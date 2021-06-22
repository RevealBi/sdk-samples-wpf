using Reveal.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace FilteringDashboards
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
            _revealView.Dashboard = new RVDashboard(filePath);
        }
        private void AllTime_Click(object sender, RoutedEventArgs e)
        {
            _revealView.Dashboard.DateFilter = new RVDateDashboardFilter(RVDateFilterType.AllTime);
        }

        private void Today_Click(object sender, RoutedEventArgs e)
        {
            _revealView.Dashboard.DateFilter = new RVDateDashboardFilter(RVDateFilterType.Today);
        }

        private void LastYear_Click(object sender, RoutedEventArgs e)
        {
            _revealView.Dashboard.DateFilter = new RVDateDashboardFilter(RVDateFilterType.LastYear);
        }

        private void Range_Click(object sender, RoutedEventArgs e)
        {
            _revealView.Dashboard.DateFilter = new RVDateDashboardFilter(RVDateFilterType.CustomRange, 
                new RVDateRange(DateTime.Now.AddDays(-100), DateTime.Now));
        }
    }
}
