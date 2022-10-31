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
            // for decimal data types
            var decimalProps = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => (System.Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

            foreach (var property in decimalProps)
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }

            modelBuilder.Entity<User>();
            modelBuilder.Entity<Interest>()
                .HasData(
                    new Interest { Id = 1, Name = "Standard", Description = "Standard Interest", DurationMin = 0, DurationMax = -1, Rate = 0.05, StartFromNMonth = 1},
                    new Interest { Id = 2, Name = "First 2 Months Free", Description = "First 2 Months Interest Free", DurationMin = 6, DurationMax = -1, Rate = 0.05, StartFromNMonth = 3}
                );
            modelBuilder.Entity<Product>()
                .HasData(
                    new Product { Id = 1, Name = "ProductA", Description = "Interest Free Loan", InterestId = null },
                    new Product { Id = 2, Name = "ProductB", Description = "First 2 Months Interest Free", InterestId = 2},
                    new Product { Id = 3, Name = "ProductA", Description = "Standard Interest", InterestId = 1}
                );
            modelBuilder.Entity<Quote>();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.EnableSensitiveDataLogging();
        }
    }
}
