using Reveal.Sdk;
using System;
using System.IO;
using System.Windows;

namespace Tooltips
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
            _revealView.TooltipShowing += RevealView_TooltipShowing;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Dashboards/Sales.rdash");
            _revealView.Dashboard = new RVDashboard(path);
        }

        private void RevealView_TooltipShowing(object sender, TooltipShowingEventArgs e)
        {
            if (e.Visualization.Title == "New vs Renewal Sales")
            {
                e.Cancel = true;
            }
        }
    }
}
