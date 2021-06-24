using Reveal.Sdk;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThemingDashboards.Business;

namespace ThemingDashboards
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            _themes.ItemsSource = ThemeItem.GenerateThemeItems();
            _revealView.Dashboard = new RVDashboard(Path.Combine(Environment.CurrentDirectory, "Dashboards/Sales.rdash"));
        }

        private void Themes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] is ThemeItem themeItem)
            {
                RevealTheme theme = RevealSdkSettings.Theme;

                //if the previous and current theme are the same, don't reapply the theme
                if (themeItem.Theme?.ToString() == theme.ToString())
                    return;

                if (themeItem.Name == "Clone Existing Theme")
                {
                    theme = CreateCustomUglyTheme();
                }
                else
                {
                    theme = themeItem.Theme;
                }

                RevealSdkSettings.Theme = theme;
                _revealView.RefreshTheme();
            }
        }

        RevealTheme CreateCustomUglyTheme()
        {
            var customTheme = RevealSdkSettings.Theme.Clone();

            customTheme.FontColor = Colors.DarkBlue;
            customTheme.AccentColor = Colors.Green;
            customTheme.DashboardBackgroundColor = Colors.LightYellow;
            customTheme.VisualizationBackgroundColor = Colors.LightGray;

            customTheme.ChartColors.Clear();
            customTheme.ChartColors.Add(Color.FromRgb(192, 80, 77));
            customTheme.ChartColors.Add(Color.FromRgb(101, 197, 235));
            customTheme.ChartColors.Add(Color.FromRgb(232, 77, 137));

            return customTheme;
        }
    }
}
