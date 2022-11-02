using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SnappetChallenge.Report.Repository
{
    public static class Configure
    {
        public static IServiceCollection AddReportRepository(this IServiceCollection services, string connectionString)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddDbContext<ReportDatabaseContext>(options =>
                   options.UseSqlServer(connectionString,
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                        sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    }));

            AddRepositoryScoped(services);

            return services;
        }

        private static void AddRepositoryScoped(IServiceCollection services)
        {
            services.AddScoped<IReportRepository, ReportRepository>();
        }

        public static IServiceCollection UseMemoryDataBase(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ReportDatabaseContext>(opt => opt.UseInMemoryDatabase(connectionString));
            AddRepositoryScoped(services);
            return services;
        }
    }
}