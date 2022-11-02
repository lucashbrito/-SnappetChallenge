using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SnappetChallenge.Report.Repository
{
    public static class Configure
    {
        public static IServiceCollection AddReportRepository(this IServiceCollection services, string connectionString, bool useMemoryDatabase)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (useMemoryDatabase)
                UseMemoryDataBase(services, connectionString);
            else
                services.AddDbContext<ReportDatabaseContext>(options =>
                       options.UseSqlServer(connectionString,
                        sqlServerOptions =>
                        {
                            sqlServerOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                            sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                        }));

            services.AddScoped<IReportRepository, ReportRepository>();

            return services;
        }

        private static void UseMemoryDataBase(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ReportDatabaseContext>(opt => opt.UseInMemoryDatabase(connectionString));
        }
    }
}