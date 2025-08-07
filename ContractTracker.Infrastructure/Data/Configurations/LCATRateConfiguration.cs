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
            
            builder.Property(r => r.RateType)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();
                
            builder.Property(r => r.Rate)
                .HasPrecision(10, 2)
                .IsRequired();
                
            builder.Property(r => r.EffectiveDate)
                .IsRequired();
                
            builder.Property(r => r.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(r => r.Notes)
                .HasMaxLength(500);
                
            builder.HasIndex(r => new { r.LCATId, r.RateType, r.EffectiveDate });
        }
    }
}