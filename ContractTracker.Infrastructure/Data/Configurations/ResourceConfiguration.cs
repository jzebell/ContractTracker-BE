using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContractTracker.Domain.Entities;

namespace ContractTracker.Infrastructure.Data.Configurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("Resources");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.LCATId);

            builder.Property(r => r.PayRate)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(r => r.BurdenedCost)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(r => r.StartDate)
                .IsRequired();

            builder.Property(r => r.EndDate);

            builder.Property(r => r.IsActive)
                .IsRequired();

            builder.Property(r => r.ClearanceLevel)
                .HasMaxLength(50);

            builder.Property(r => r.ClearanceExpirationDate);

            builder.Property(r => r.Location)
                .HasMaxLength(100);

            builder.Property(r => r.Notes)
                .HasMaxLength(1000);

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.Property(r => r.UpdatedAt)
                .IsRequired();

            builder.Property(r => r.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.ModifiedBy)
                .IsRequired()
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(r => r.LCAT)
                .WithMany()
                .HasForeignKey(r => r.LCATId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(r => r.ContractResources)
                .WithOne(cr => cr.Resource)
                .HasForeignKey(cr => cr.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(r => r.Email)
                .IsUnique();

            builder.HasIndex(r => r.IsActive)
                .HasDatabaseName("IX_Resources_IsActive");
        }
    }
}