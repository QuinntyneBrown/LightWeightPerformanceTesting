using System.Threading;
using System.Threading.Tasks;
using LightWeightPerformanceTesting.Core.Interfaces;
using LightWeightPerformanceTesting.Core.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LightWeightPerformanceTesting.Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options)
            :base(options) { }

        public DbSet<StoredEvent> StoredEvents { get; set; }
    }
}
