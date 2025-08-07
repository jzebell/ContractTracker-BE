using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContractTracker.Domain.Entities;

namespace ContractTracker.Infrastructure.Data.Configurations
{
    public class ContractModificationConfiguration : IEntityTypeConfiguration<ContractModification>
    {
        public void Configure(EntityTypeBuilder<ContractModification> builder)
        {
            builder.ToTable("ContractModifications");

            builder.HasKey(cm => cm.Id);

            builder.Property(cm => cm.ContractId)
                .IsRequired();

            builder.Property(cm => cm.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(cm => cm.OldValue)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(cm => cm.NewValue)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(cm => cm.Justification)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(cm => cm.ModificationDate)
                .IsRequired();

            builder.Property(cm => cm.ModifiedBy)
                .IsRequired()
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(cm => cm.Contract)
                .WithMany(c => c.ContractModifications)
                .HasForeignKey(cm => cm.ContractId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index
            builder.HasIndex(cm => new { cm.ContractId, cm.ModificationDate })
                .HasDatabaseName("IX_ContractModifications_Contract_Date");
        }
    }
}