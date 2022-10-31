using Microsoft.EntityFrameworkCore;
using MoneyMe.Repositories.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Repositories.Data
{
    public class MoneyMeContext : DbContext
    {
        public MoneyMeContext(DbContextOptions<MoneyMeContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Interest>();
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<Quote>();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.EnableSensitiveDataLogging();
        }
    }
}
