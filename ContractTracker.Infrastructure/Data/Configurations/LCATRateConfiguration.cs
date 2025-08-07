using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContractTracker.Domain.Entities;

namespace ContractTracker.Infrastructure.Data.Configurations
{
    public class LCATRateConfiguration : IEntityTypeConfiguration<LCATRate>
    {
        public void Configure(EntityTypeBuilder<LCATRate> builder)
        {
            builder.ToTable("LCATRates");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.LCATId)
                .IsRequired();

            builder.Property(r => r.Rate)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(r => r.RateType)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(r => r.EffectiveDate)
                .IsRequired();

            builder.Property(r => r.EndDate);

            builder.Property(r => r.Notes)
                .HasMaxLength(500);

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.Property(r => r.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(r => r.LCAT)
                .WithMany(l => l.Rates)
                .HasForeignKey(r => r.LCATId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(r => new { r.LCATId, r.RateType, r.EffectiveDate })
                .HasDatabaseName("IX_LCATRates_LCAT_Type_EffectiveDate");
        }
    }
}