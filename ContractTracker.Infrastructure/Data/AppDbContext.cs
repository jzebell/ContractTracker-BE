// ContractTracker.Infrastructure/Data/AppDbContext.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;  // Add this for ValueConverter
using ContractTracker.Domain.Entities;

namespace ContractTracker.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Core entities
        public DbSet<LCAT> LCATs { get; set; }
        public DbSet<Resource> Resources { get; set; }
        
        // Contract entities
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractLCAT> ContractLCATs { get; set; }
        public DbSet<ContractResource> ContractResources { get; set; }
        public DbSet<ContractModification> ContractModifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure all DateTime properties to be treated as UTC
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(
                            new ValueConverter<DateTime, DateTime>(
                                v => v.Kind == DateTimeKind.Unspecified 
                                    ? DateTime.SpecifyKind(v, DateTimeKind.Utc) 
                                    : v.ToUniversalTime(),
                                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                    }
                    else if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(
                            new ValueConverter<DateTime?, DateTime?>(
                                v => v.HasValue 
                                    ? v.Value.Kind == DateTimeKind.Unspecified 
                                        ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) 
                                        : v.Value.ToUniversalTime()
                                    : v,
                                v => v.HasValue 
                                    ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) 
                                    : v));
                    }
                }
            }
            
            // Contract number should be unique
            modelBuilder.Entity<Contract>()
                .HasIndex(c => c.ContractNumber)
                .IsUnique();
                
            // Enum conversions
            modelBuilder.Entity<Contract>()
                .Property(e => e.Status)
                .HasConversion<string>();
                
            modelBuilder.Entity<Contract>()
                .Property(e => e.ContractType)
                .HasConversion<string>();
                
            modelBuilder.Entity<ContractModification>()
                .Property(e => e.Type)
                .HasConversion<string>();
                
            modelBuilder.Entity<Resource>()
                .Property(e => e.ResourceType)
                .HasConversion<string>();
        }
    }
}