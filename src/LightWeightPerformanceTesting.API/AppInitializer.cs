using LightWeightPerformanceTesting.Core.Identity;
using LightWeightPerformanceTesting.Core.Interfaces;
using LightWeightPerformanceTesting.Core.Models;
using LightWeightPerformanceTesting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace LightWeightPerformanceTesting.API
{
    public class AppInitializer : IDesignTimeDbContextFactory<AppDbContext>
    {
        public static void Seed(
            IDateTime dateTime,
            IEventStore eventStore,
            IServiceScopeFactory services,
            IRepository repository)
        {
            CardConfiguration.Seed(dateTime, eventStore, repository);
            UserConfiguration.Seed(dateTime, eventStore, repository);
        }

        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddUserSecrets(typeof(Startup).GetTypeInfo().Assembly)
                .Build();

            return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"])
                .Options);
        }
    }


    internal class UserConfiguration
    {
        public static void Seed(IDateTime dateTime, IEventStore eventStore, IRepository repository)
        {
            if (repository.Query<User>().SingleOrDefault(x => x.Username == "quinntynebrown@gmail.com") == null)
            {
                var salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                var hashedPassword = new PasswordHasher().HashPassword(salt, "P@ssw0rd");

                var user = new User("quinntynebrown@gmail.com", salt, hashedPassword);

                var dashboard = new Dashboard("Default", user.UserId);

                eventStore.Save(user);
                eventStore.Save(dashboard);
            }            
        }
    }


    internal class RoleConfiguration
    {

    }

    internal class CardConfiguration
    {
        public static void Seed(IDateTime dateTime, IEventStore eventStore, IRepository repository)
        {
            if (repository.Query<Card>().SingleOrDefault(x => x.Name == "Light Weight Performance Test1") == null)
                eventStore.Save(new Card("Light Weight Performance Test1", CardType.LightWeightPerformanceTest));

            if (repository.Query<Card>().SingleOrDefault(x => x.Name == "Light Weight Performance Test") == null)
                eventStore.Save(new Card("Light Weight Performance Test", CardType.LightWeightPerformanceTest));

        }
    }

    internal class DashboardConfiguration
    {
        public static void Seed(IDateTime dateTime, IEventStore eventStore, IRepository repository)
        {
        }
    }

    internal class DashboardTileConfiguration
    {
        public static void Seed(IDateTime dateTime, IEventStore eventStore, IRepository repository)
        {
        }
    }    
}
