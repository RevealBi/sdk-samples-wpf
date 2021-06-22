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

        private void SetJapanFilter_Click(object sender, RoutedEventArgs e)
        {
            var territoryFilter = _revealView.Dashboard.Filters.GetByTitle("Territory");
            territoryFilter.SelectedValues = new List<object>() { "Japan" };
        }

        private void SetMutipleFilters_Click(object sender, RoutedEventArgs e)
        {
            var territoryFilter = _revealView.Dashboard.Filters.GetByTitle("Territory");
            territoryFilter.SelectedValues = new List<object>() { "Japan", "India" };
        }

        private async void ListTerritoryFilterValues_Click(object sender, RoutedEventArgs e)
        {
            var territoryFilter = _revealView.Dashboard.Filters.GetByTitle("Territory");
            var filterValues = await territoryFilter.GetFilterValuesAsync();
            _filterList.ItemsSource = filterValues;
        }

        private void FilterList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var filterValue = e.AddedItems[0] as RVFilterValue;
            var territoryFilter = _revealView.Dashboard.Filters.GetByTitle("Territory");
            territoryFilter.SelectedValues = new List<object>() { filterValue.Value };
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            var territoryFilter = _revealView.Dashboard.Filters.GetByTitle("Territory");
            territoryFilter.SelectedValues = new List<object>();
        }
    }
}
