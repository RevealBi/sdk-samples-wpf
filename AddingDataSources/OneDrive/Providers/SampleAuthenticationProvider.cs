using Microsoft.Identity.Client;
using Reveal.Sdk;
using Reveal.Sdk.Data;
using Reveal.Sdk.Data.Microsoft.OneDrive;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace DataSource_OneDrive
{
    internal class SampleAuthenticationProvider : IRVAuthenticationProvider
    {
        string[] oneDriveScopes = new string[] { "openid", "offline_access", "https://graph.microsoft.com/User.Read", "https://graph.microsoft.com/Files.Read.All" };

        public async Task<IRVDataSourceCredential> ResolveCredentialsAsync(RVDashboardDataSource dataSource)
        {
            if (dataSource is RVOneDriveDataSource)
            {
                var authentication = await OneDriveAuthenticate();
                if (authentication != null)
                {
                    return new RVBearerTokenDataSourceCredential(authentication.AccessToken, authentication.Account.Username);
                }
            }
            return null;
        }

        private async Task<AuthenticationResult> OneDriveAuthenticate()
        {
            AuthenticationResult authResult = null;
            var app = App.PublicClientApp;

            var accounts = await app.GetAccountsAsync();
            var firstAccount = accounts.FirstOrDefault();

            try
            {
                authResult = await app.AcquireTokenSilent(oneDriveScopes, firstAccount)
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                // A MsalUiRequiredException happened on AcquireTokenSilent. 
                // This indicates you need to call AcquireTokenInteractive to acquire a token
                System.Diagnostics.Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

                try
                {
                    authResult = await app.AcquireTokenInteractive(oneDriveScopes)
                        .WithAccount(firstAccount)
                        .WithParentActivityOrWindow(new WindowInteropHelper(Application.Current.MainWindow).Handle) // optional, used to center the browser on the window
                        .WithPrompt(Prompt.SelectAccount)
                        .ExecuteAsync();
                }
                catch (Exception msalex)
                {
                    Console.WriteLine($"Error Acquiring Token:{System.Environment.NewLine}{msalex}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}");
                return null;
            }
            return authResult;
        }
    }
}