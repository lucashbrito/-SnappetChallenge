
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SnappetChallenge.Report.Repository
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReportDatabaseContext>
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.Local.json", optional: false, reloadOnChange: true)
        .Build();

        public ReportDatabaseContext CreateDbContext(string[] args)
        {
            var client = new SecretClient(new Uri(Configuration["Azure:KeyVault:Address"]), new DefaultAzureCredential() { },

                new SecretClientOptions()
                {
                    Retry = {
                    Delay= TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
              }
                });

            //Server=tcp:snappet-sql-server-dev.database.windows.net,1433;Initial Catalog=report;Persist Security Info=False;User ID=admin.lucas;Password=brito123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
            //
            var general = client.GetSecret("Repositories--Report--ConnectionString");

            var optionsBuilder = new DbContextOptionsBuilder<ReportDatabaseContext>();
            optionsBuilder.UseSqlServer(general.Value.Value.ToString());

            return new ReportDatabaseContext(optionsBuilder.Options);

        }
    }
}
