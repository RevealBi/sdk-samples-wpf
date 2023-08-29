using Reveal.Sdk;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace ChartTypes
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

            //set default chart type
            _revealView.DefaultChartType = RVChartType.StackedColumnChart;

            //update bar chart type
            var barConfig = _revealView.ChartTypes.First(x => x.ChartType == RVChartType.BarChart);
            barConfig.Title = "My Cool Bar";
            barConfig.Icon = "https://help.revealbi.io/img/logo.png";
            barConfig.Groups = new string[] { "Enterprise Visualizations" };

            //remove grid chart type
            _revealView.ChartTypes.Remove(_revealView.ChartTypes.FirstOrDefault(x => x.ChartType == RVChartType.Grid));

            //add custom vizualization       
            _revealView.ChartTypes.Add(new RVChartTypeCustomItem("Custom Viz", "https://host/customViz.html")
            {
                Icon = "https://help.revealbi.io/img/logo.png",
                Groups = new string[] { "Custom Vizualizations" }
            });
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards/Sales.rdash");
            _revealView.Dashboard = new RVDashboard(filePath);
        }
    }
}
