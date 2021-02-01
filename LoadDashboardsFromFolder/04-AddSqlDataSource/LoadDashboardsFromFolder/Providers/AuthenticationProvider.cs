using Reveal.Sdk;
using System.Threading.Tasks;

namespace LoadDashboardsFromFolder.Providers
{
    public class AuthenticationProvider : IRVAuthenticationProvider
    {
        public Task<IRVDataSourceCredential> ResolveCredentialsAsync(RVDashboardDataSource dataSource)
        {
            return Task.FromResult<IRVDataSourceCredential>(new RVUsernamePasswordDataSourceCredential("your-username", "your-password"));
        }
    }
}
