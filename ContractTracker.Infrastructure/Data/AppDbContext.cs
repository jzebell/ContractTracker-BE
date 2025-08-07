using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ContractTracker.Domain.Entities;

namespace ContractTracker.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets for all entities
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractLCAT> ContractLCATs { get; set; }
        public DbSet<ContractResource> ContractResources { get; set; }
        public DbSet<ContractModification> ContractModifications { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<LCAT> LCATs { get; set; }
        public DbSet<LCATRate> LCATRates { get; set; }
        public DbSet<PositionTitle> PositionTitles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations from the assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Global DateTime UTC conversion for PostgreSQL
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                            v => v.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v, DateTimeKind.Utc),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                    }
                    else if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(new ValueConverter<DateTime?, DateTime?>(
                            v => v.HasValue 
                                ? (v.Value.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v.Value, DateTimeKind.Utc)) 
                                : v,
                            v => v.HasValue 
                                ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) 
                                : v));
                    }
                }
            }

            // Set default values for audit fields
            // LCAT entity
            modelBuilder.Entity<LCAT>()
                .Property(e => e.CreatedBy)
                .HasDefaultValue("System");

            modelBuilder.Entity<LCAT>()
                .Property(e => e.ModifiedBy)
                .HasDefaultValue("System");

            // Resource entity
            modelBuilder.Entity<Resource>()
                .Property(e => e.CreatedBy)
                .HasDefaultValue("System");

            modelBuilder.Entity<Resource>()
                .Property(e => e.ModifiedBy)
                .HasDefaultValue("System");

            // Contract entity
            modelBuilder.Entity<Contract>()
                .Property(e => e.CreatedBy)
                .HasDefaultValue("System");

            modelBuilder.Entity<Contract>()
                .Property(e => e.ModifiedBy)
                .HasDefaultValue("System");

            // ContractResource entity
            modelBuilder.Entity<ContractResource>()
                .Property(e => e.CreatedBy)
                .HasDefaultValue("System");

            modelBuilder.Entity<ContractResource>()
                .Property(e => e.ModifiedBy)
                .HasDefaultValue("System");

            // ContractModification entity
            modelBuilder.Entity<ContractModification>()
                .Property(e => e.ModifiedBy)
                .HasDefaultValue("System");

            // Seed initial data if needed
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // You can add seed data here if needed
            // For example, default LCATs, standard contract types, etc.
            
            // Example seed data (uncomment if needed):
            /*
            var lcatId = Guid.NewGuid();
            modelBuilder.Entity<LCAT>().HasData(
                new 
                {
                    Id = lcatId,
                    Code = "SE3",
                    Title = "Software Engineer III",
                    Description = "Senior Software Engineer",
                    Category = "Engineering",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    ModifiedBy = "System"
                }
            );
            */
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                // Update UpdatedAt for all modified entities that have this property
                if (entry.State == EntityState.Modified)
                {
                    var updatedAtProperty = entry.Properties
                        .FirstOrDefault(p => p.Metadata.Name == "UpdatedAt");
                    
                    if (updatedAtProperty != null)
                    {
                        updatedAtProperty.CurrentValue = DateTime.UtcNow;
                    }

                    var modifiedByProperty = entry.Properties
                        .FirstOrDefault(p => p.Metadata.Name == "ModifiedBy");
                    
                    if (modifiedByProperty != null && string.IsNullOrEmpty(modifiedByProperty.CurrentValue?.ToString()))
                    {
                        modifiedByProperty.CurrentValue = "System";
                    }
                }

                // Set CreatedAt and CreatedBy for new entities
                if (entry.State == EntityState.Added)
                {
                    var createdAtProperty = entry.Properties
                        .FirstOrDefault(p => p.Metadata.Name == "CreatedAt");
                    
                    if (createdAtProperty != null && (DateTime)createdAtProperty.CurrentValue == default)
                    {
                        createdAtProperty.CurrentValue = DateTime.UtcNow;
                    }

                    var updatedAtProperty = entry.Properties
                        .FirstOrDefault(p => p.Metadata.Name == "UpdatedAt");
                    
                    if (updatedAtProperty != null && (DateTime)updatedAtProperty.CurrentValue == default)
                    {
                        updatedAtProperty.CurrentValue = DateTime.UtcNow;
                    }

                    var createdByProperty = entry.Properties
                        .FirstOrDefault(p => p.Metadata.Name == "CreatedBy");
                    
                    if (createdByProperty != null && string.IsNullOrEmpty(createdByProperty.CurrentValue?.ToString()))
                    {
                        createdByProperty.CurrentValue = "System";
                    }

                    var modifiedByProperty = entry.Properties
                        .FirstOrDefault(p => p.Metadata.Name == "ModifiedBy");
                    
                    if (modifiedByProperty != null && string.IsNullOrEmpty(modifiedByProperty.CurrentValue?.ToString()))
                    {
                        modifiedByProperty.CurrentValue = "System";
                    }
                }
            }
        }
    }
}