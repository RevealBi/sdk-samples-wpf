using LoadDashboardsFromFolder.Business;
using Reveal.Sdk;
using System.Windows;

namespace LoadDashboardsFromFolder
{
    /// <summary>
    /// Interaction logic for ThemeWindow.xaml
    /// </summary>
    public partial class ThemeWindow : Window
    {
        public RevealTheme Theme { get; set; }

        public ThemeWindow(RevealTheme theme)
        {
            InitializeComponent();
            Theme = ThemeItem.CloneTheme(theme); //work against a copy of the theme, not the instance
            _propertyGrid.SelectedObject = Theme;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
