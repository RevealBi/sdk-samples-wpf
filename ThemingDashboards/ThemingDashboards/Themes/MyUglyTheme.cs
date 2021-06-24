using Reveal.Sdk;
using System;
using System.Windows.Media;

namespace ThemingDashboards.Themes
{
    public class MyUglyTheme : RevealTheme
    {
        public MyUglyTheme()
        {
            var regularFont = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#Wingdings 2");
            var mediumFont = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#Lucida Calligraphy");
            var boldFont = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CF Crack and");

            RegularFont = regularFont;
            MediumFont = mediumFont;
            BoldFont = boldFont;

            FontColor = Colors.DarkRed;
            AccentColor = Color.FromRgb(192, 80, 77);
            DashboardBackgroundColor = Colors.Black;
            VisualizationBackgroundColor = Color.FromRgb(153, 255, 255);

            ChartColors.Clear();
            ChartColors.Add(Color.FromRgb(192, 80, 77));
            ChartColors.Add(Color.FromRgb(101, 197, 235));
            ChartColors.Add(Color.FromRgb(232, 77, 137));

            UseRoundedCorners = false;
        }
    }
}
