using Microsoft.EntityFrameworkCore;
using Vilarim.POC.YouTube.Domain.Entities;

namespace Vilarim.POC.YouTube.Infra
{
    public class YouTubeContext : DbContext
    {
        private DbContextOptions _inMemoryDatabase;
        protected YouTubeContext(DbContextOptions options) : base(options) { }

        public YouTubeContext(DbContextOptions<DbContext> options) : base(options)
        {
           _inMemoryDatabase = options;//.FindExtension<InMemoryOptionsExtension>()?.StoreName == "TestScope";
        }

        public virtual DbSet<ResponseSearchItem> ResponseSearchItem { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}