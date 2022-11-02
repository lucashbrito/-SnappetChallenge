using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.DependencyInjection;
using SnappetChallenge.Report.Repository;
using SnappetChallenge.Report.Service;

namespace SnappetChallenge.Report.Common
{
    public static class DependencyInjection
    {
        public async static Task<IServiceCollection> AddReportDependencyInjection(this IServiceCollection services, string keyVaultAddress)
        {
            var client = new SecretClient(new Uri(keyVaultAddress), new DefaultAzureCredential() { },
                new SecretClientOptions()
                {
                    Retry = {
                    Delay= TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                }
                });

            var general = await client.GetSecretAsync("Repositories--Report--ConnectionString");

            Configure.AddReportRepository(services, general?.Value?.Value?.ToString(), false);

            services.AddScoped<IReportService, ReportService>();

            return services;
        }
    }
}