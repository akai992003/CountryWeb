﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace CountryWeb.Data {
    public class TgContext : DbContext {
        public static string ConnS;
        public TgContext () {
        }

        public TgContext (DbContextOptions<TgContext> options) : base (options) {
            var sqlServerOptionsExtension = options.FindExtension<SqlServerOptionsExtension> ();
            if (sqlServerOptionsExtension != null) {
                ConnS = sqlServerOptionsExtension.ConnectionString;
            }
        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer (ConnS);
        }

        public DbSet<newsLists> newsLists { get; set; }
        public DbSet<Covid19> Covid19 { get; set; }
        public DbSet<vP> vP { get; set; }
        
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
        }
    }

}