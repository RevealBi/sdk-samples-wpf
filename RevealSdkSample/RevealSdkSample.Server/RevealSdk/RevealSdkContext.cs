using Infragistics.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RevealSdkSample.Server.RevealSdk
{
    public class RevealSdkContext : IRevealSdkContext
    {
        public RevealSdkContext()
        {
            var liveDashboardsLocation = "LiveDashboards/";
            Directory.CreateDirectory(liveDashboardsLocation);

            var assembly = Assembly.GetExecutingAssembly();
            var embeddedResources = assembly.GetManifestResourceNames();

            foreach (var rdashPath in embeddedResources.Where(path => path.Contains(".rdash")))
            {
                var stream = assembly.GetManifestResourceStream(rdashPath);
                var fileName = rdashPath.Split('.').Skip(3).First() +".rdash";
                var fullPath = Path.Combine(liveDashboardsLocation, fileName);
                using (var output = File.Open(fullPath, FileMode.Create ))
                {
                    stream.CopyTo(output);
                }
            }

        }
        public IRVDataSourceProvider DataSourceProvider => null;

        public IRVDataProvider DataProvider => null;

        public IRVAuthenticationProvider AuthenticationProvider => null;

        public async Task<Stream> GetDashboardAsync(string dashboardId)
        {
            return await Task.Run(() =>
            {
                var fileName = dashboardId.Split('|')[0];
                var dashboardFileName = fileName + ".rdash";
                var rdashLocation = "LiveDashboards/" + dashboardFileName;

                MemoryStream memStream = new MemoryStream();
                using (var fileStream = File.OpenRead(rdashLocation))
                {
                    fileStream.CopyTo(memStream);
                }

                return memStream;
            });
        }

        public async Task SaveDashboardAsync(string userId, string dashboardId, Stream dashboardStream)
        {
            var liveDashboardsLocation = "LiveDashboards/";
            var rdashTargetPath = Path.Combine(liveDashboardsLocation, dashboardId + ".rdash");

            using (var output = File.Open(rdashTargetPath, FileMode.Create))
            {
                dashboardStream.CopyTo(output);
            }
        }
    }
}
