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
        public IRVDataSourceProvider DataSourceProvider => null;

        public IRVDataProvider DataProvider => null;

        public IRVAuthenticationProvider AuthenticationProvider => null;

        public async Task<Stream> GetDashboardAsync(string dashboardId)
        {
            return await Task.Run(() =>
            {
                var fileName = dashboardId.Split('|')[0];
                var dashboardFileName = fileName + ".rdash";
                var resourceName = $"RevealSdkSample.Server.Dashboards.{dashboardFileName}";

                var assembly = Assembly.GetExecutingAssembly();
                var a = assembly.GetManifestResourceNames();
                return assembly.GetManifestResourceStream(resourceName);
            });
        }

        public async Task SaveDashboardAsync(string userId, string dashboardId, Stream dashboardStream)
        {
            await Task.Run(() => { });
            //var dashboardFileName = dashboardId + ".rdash";
            //var resourceName = $"Reveal.WebSdk.Sample.Core461.Dashboards.{dashboardFileName}";

            //var assembly = Assembly.GetExecutingAssembly();
            //var a = assembly.GetManifestResourceNames();
            //return assembly.GetManifestResourceStream(resourceName);
        }
    }
}
