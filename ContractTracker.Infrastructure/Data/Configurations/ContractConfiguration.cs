using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContractTracker.Domain.Entities;

namespace ContractTracker.Infrastructure.Data.Configurations
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contracts");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.ContractNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.CustomerName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.PrimeContractorName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.IsPrime)
                .IsRequired();

            builder.Property(c => c.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(c => c.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(c => c.StartDate)
                .IsRequired();

            builder.Property(c => c.EndDate)
                .IsRequired();

            builder.Property(c => c.TotalValue)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(c => c.FundedValue)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(c => c.StandardFullTimeHours)
                .IsRequired()
                .HasDefaultValue(1912);

            builder.Property(c => c.Description)
                .HasMaxLength(1000);

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .IsRequired();

            builder.Property(c => c.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.ModifiedBy)
                .IsRequired()
                .HasMaxLength(100);

            // Relationships
            builder.HasMany(c => c.ContractLCATs)
                .WithOne(cl => cl.Contract)
                .HasForeignKey(cl => cl.ContractId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.ContractResources)
                .WithOne(cr => cr.Contract)
                .HasForeignKey(cr => cr.ContractId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.ContractModifications)
                .WithOne(cm => cm.Contract)
                .HasForeignKey(cm => cm.ContractId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index
            builder.HasIndex(c => c.ContractNumber)
                .IsUnique();
        }
    }
}