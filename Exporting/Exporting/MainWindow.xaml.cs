using Reveal.Sdk;
using System;
using System.IO;
using System.Windows;

namespace Exporting
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
    }
}
