using Reveal.Sdk;
using System.Collections.Generic;

namespace LoadDashboardsFromFolder.Business
{
    public class ThemeItem
    {
        public string Name { get; set; }

        public RevealTheme Theme { get; set; }

        public static IEnumerable<ThemeItem> GenerateThemeItems()
        {
            List<ThemeItem> themes = new List<ThemeItem>();

            themes.Add(new ThemeItem() { Name = "Mountain Light", Theme = new MountainLightTheme() });
            themes.Add(new ThemeItem() { Name = "Mountain Dark", Theme = new MountainDarkTheme() });
            themes.Add(new ThemeItem() { Name = "Ocean Light", Theme = new OceanLightTheme() });
            themes.Add(new ThemeItem() { Name = "Ocean Dark", Theme = new OceanDarkTheme() });
            themes.Add(new ThemeItem() { Name = "Custom", Theme = null });
            return themes;
        }

        public static RevealTheme CloneTheme(RevealTheme theme)
        {
            var newTheme = new RevealTheme();
            newTheme.AccentColor = theme.AccentColor;
            newTheme.BoldFont = theme.BoldFont;
            newTheme.ChartColors.Clear();
            newTheme.ChartColors.AddRange(theme.ChartColors);
            newTheme.ConditionalFormatting.MIDColor = theme.ConditionalFormatting.MIDColor;
            newTheme.ConditionalFormatting.HIColor = theme.ConditionalFormatting.HIColor;
            newTheme.ConditionalFormatting.LOWColor = theme.ConditionalFormatting.LOWColor;
            newTheme.ConditionalFormatting.NONEColor = theme.ConditionalFormatting.NONEColor;
            newTheme.DashboardBackgroundColor = theme.DashboardBackgroundColor;
            newTheme.FontColor = theme.FontColor;
            newTheme.HighlightColor = theme.HighlightColor;
            newTheme.MediumFont = theme.MediumFont;
            newTheme.RegularFont = theme.RegularFont;
            newTheme.UseRoundedCorners = theme.UseRoundedCorners;
            newTheme.VisualizationBackgroundColor = theme.VisualizationBackgroundColor;
            return newTheme;
        }
    }
}
