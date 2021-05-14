using Reveal.Sdk;
using System.Reflection;
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
            var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream($"LoadingDashboards.Dashboards.Sales.rdash");
            using (resource)
            {
                _revealView.Dashboard = new RVDashboard(resource);
            }
        }
    }
}
