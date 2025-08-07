using Microsoft.EntityFrameworkCore;
using ContractTracker.Domain.Entities;
using System;
using System.Reflection;

namespace ContractTracker.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<LCAT> LCATs { get; set; }
        public DbSet<LCATRate> LCATRates { get; set; }
        public DbSet<PositionTitle> PositionTitles { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractLCATRate> ContractLCATRates { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceTypeDefinition> ResourceTypeDefinitions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations from this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}