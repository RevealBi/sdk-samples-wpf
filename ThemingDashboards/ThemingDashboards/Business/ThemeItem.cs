using Reveal.Sdk;
using System.Collections.Generic;
using ThemingDashboards.Themes;

namespace ThemingDashboards.Business
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
            themes.Add(new ThemeItem() { Name = "My Ugly Theme", Theme = new MyUglyTheme() });
            themes.Add(new ThemeItem() { Name = "Clone Existing Theme", Theme = null });
            return themes;
        }
    }
}
