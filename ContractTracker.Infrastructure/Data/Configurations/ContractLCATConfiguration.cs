using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContractTracker.Domain.Entities;

namespace ContractTracker.Infrastructure.Data.Configurations
{
    public class ContractLCATConfiguration : IEntityTypeConfiguration<ContractLCAT>
    {
        public void Configure(EntityTypeBuilder<ContractLCAT> builder)
        {
            builder.ToTable("ContractLCATs");

            builder.HasKey(cl => cl.Id);

            builder.Property(cl => cl.ContractId)
                .IsRequired();

            builder.Property(cl => cl.LCATId)
                .IsRequired();

            builder.Property(cl => cl.OverrideRate)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(cl => cl.EffectiveDate)
                .IsRequired();

            builder.Property(cl => cl.EndDate);

            builder.Property(cl => cl.IsActive)
                .IsRequired();

            builder.Property(cl => cl.CreatedAt)
                .IsRequired();

            builder.Property(cl => cl.UpdatedAt)
                .IsRequired();

            // Relationships
            builder.HasOne(cl => cl.Contract)
                .WithMany(c => c.ContractLCATs)
                .HasForeignKey(cl => cl.ContractId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cl => cl.LCAT)
                .WithMany()
                .HasForeignKey(cl => cl.LCATId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(cl => new { cl.ContractId, cl.LCATId, cl.EffectiveDate })
                .HasDatabaseName("IX_ContractLCATs_Contract_LCAT_EffectiveDate");
        }
    }
}