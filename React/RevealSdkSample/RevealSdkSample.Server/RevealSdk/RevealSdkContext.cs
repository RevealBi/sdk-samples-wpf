using Reveal.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RevealSdkSample.Server.RevealSdk
{
    public class RevealSdkContext : RevealSdkContextBase
    {
        public RevealSdkContext()
        {
            var liveDashboardsLocation = "LiveDashboards/";
            if (Directory.Exists(liveDashboardsLocation))
            {
                Console.WriteLine("Dashboards present!");

            }
            else
            {
                Directory.CreateDirectory(liveDashboardsLocation);
                Console.WriteLine("Dahboards missing. Initializing!");
                var assembly = Assembly.GetExecutingAssembly();
                var embeddedResources = assembly.GetManifestResourceNames();

                foreach (var rdashPath in embeddedResources.Where(path => path.Contains(".rdash")))
                {
                    var stream = assembly.GetManifestResourceStream(rdashPath);
                    var fileName = rdashPath.Split('.').Skip(3).First() + ".rdash";
                    var fullPath = Path.Combine(liveDashboardsLocation, fileName);
                    using (var output = File.Open(fullPath, FileMode.Create))
                    {
                        stream.CopyTo(output);
                    }
                }
            }
        }
        public override IRVDataSourceProvider DataSourceProvider => null;

        public override IRVDataProvider DataProvider => null;

        public override IRVAuthenticationProvider AuthenticationProvider => null;

        public override async Task<Dashboard> GetDashboardAsync(string dashboardId)
        {
            var fileName = dashboardId.Split('|')[0];
            var dashboardFileName = fileName + ".rdash";
            var rdashLocation = "LiveDashboards/" + dashboardFileName;

            MemoryStream memStream = new MemoryStream();
            using (var fileStream = File.OpenRead(rdashLocation))
            {
                await fileStream.CopyToAsync(memStream);
            }
            memStream.Position = 0;
            return new Dashboard(memStream);
        }

        public override async Task SaveDashboardAsync(string userId, string dashboardId, Dashboard dashboard)
        { 
            var liveDashboardsLocation = "LiveDashboards/";
            var rdashTargetPath = Path.Combine(liveDashboardsLocation, dashboardId + ".rdash");

            using (var output = File.Open(rdashTargetPath, FileMode.Create))
            {
                await (await dashboard.SerializeAsync()).CopyToAsync(output);
            }
        }
    }
}
