using LightWeightPerformanceTesting.Core.Interfaces;
using LightWeightPerformanceTesting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LightWeightPerformanceTesting.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {                
        public static IServiceCollection AddDataStore(this IServiceCollection services,
                                               string connectionString, bool useInMemoryDatabase = false)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();

            return services.AddDbContext<AppDbContext>(options =>
            {
                _ = useInMemoryDatabase 
                ? options.UseInMemoryDatabase(databaseName: "LightWeightPerformanceTesting")
                : options.UseSqlServer(connectionString, b => b.MigrationsAssembly("LightWeightPerformanceTesting.Infrastructure"));
            });          
        }
    }
}
