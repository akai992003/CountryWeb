using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace CountryWeb.Data
{
    public class TgContext : DbContext
    {
        public static string ConnS;
        public TgContext()
        {
        }

        public TgContext(DbContextOptions<TgContext> options) : base(options)
        {
            var sqlServerOptionsExtension = options.FindExtension<SqlServerOptionsExtension>();
            if (sqlServerOptionsExtension != null)
            {
                ConnS = sqlServerOptionsExtension.ConnectionString;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnS);
        }

        public DbSet<newsLists> newsLists { get; set; }

        // * Echo 2021-08-08 Rename
        public DbSet<Covid19_AZ> Covid19_AZ { get; set; }
        public DbSet<VP_AZ> VP_AZ { get; set; }
        // public DbSet<Covid19> Covid19 { get; set; }
        // public DbSet<VP> VP { get; set; }

        // * Echo 2021-08-08 Add
        public DbSet<Covid19_MO> Covid19_MO { get; set; }
        public DbSet<VP_MO> VP_MO { get; set; }
        public DbSet<VPCategory> VPCategory { get; set; }
        public DbSet<A2E> A2E { get; set; }
        public DbSet<NHIQP701> NHIQP701 { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }

}