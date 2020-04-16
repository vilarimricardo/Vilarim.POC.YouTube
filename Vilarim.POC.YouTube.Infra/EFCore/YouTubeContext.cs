using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;
using Vilarim.POC.YouTube.Domain.Entities;

namespace Vilarim.POC.YouTube.Infra
{
    public class YouTubeContext : DbContext
    {
        private bool _inMemoryDatabase;

        public YouTubeContext(DbContextOptions<DbContext> options) : base(options)
        {
            _inMemoryDatabase = options.FindExtension<InMemoryOptionsExtension>()?.StoreName == "TestScope";
        }

        public DbSet<ResponseSearchItem> ResponseSearchItem { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ResponseSearchItemMap(_inMemoryDatabase));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            // if (!_inMemoryDatabase)
            // {
            //     var config = new ConfigurationBuilder()
            //    .SetBasePath(_env.ContentRootPath)
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //     // define the database to use
            //     optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            // }
        }
    }
}