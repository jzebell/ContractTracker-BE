using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContractTracker.Domain.Entities;

namespace ContractTracker.Infrastructure.Data.Configurations
{
    public class ContractResourceConfiguration : IEntityTypeConfiguration<ContractResource>
    {
        public void Configure(EntityTypeBuilder<ContractResource> builder)
        {
            builder.ToTable("ContractResources");

            builder.HasKey(cr => cr.Id);

            builder.Property(cr => cr.ContractId)
                .IsRequired();

            builder.Property(cr => cr.ResourceId)
                .IsRequired();

            builder.Property(cr => cr.AllocationPercentage)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Property(cr => cr.AnnualHours)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(cr => cr.StartDate)
                .IsRequired();

            builder.Property(cr => cr.EndDate);

            builder.Property(cr => cr.IsActive)
                .IsRequired();

            builder.Property(cr => cr.CreatedAt)
                .IsRequired();

            builder.Property(cr => cr.UpdatedAt)
                .IsRequired();

            builder.Property(cr => cr.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cr => cr.ModifiedBy)
                .IsRequired()
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(cr => cr.Contract)
                .WithMany(c => c.ContractResources)
                .HasForeignKey(cr => cr.ContractId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cr => cr.Resource)
                .WithMany(r => r.ContractResources)
                .HasForeignKey(cr => cr.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(cr => new { cr.ContractId, cr.ResourceId, cr.IsActive })
                .HasDatabaseName("IX_ContractResources_Contract_Resource_Active");
        }
    }
}