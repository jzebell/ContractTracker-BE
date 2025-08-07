using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContractTracker.Domain.Entities;

namespace ContractTracker.Infrastructure.Data.Configurations
{
    public class ContractLCATRateConfiguration : IEntityTypeConfiguration<ContractLCATRate>
    {
        public void Configure(EntityTypeBuilder<ContractLCATRate> builder)
        {
            builder.ToTable("ContractLCATRates");
            builder.HasKey(r => r.Id);
            
            builder.Property(r => r.BillRate)
                .HasPrecision(10, 2)
                .IsRequired();
                
            builder.Property(r => r.EffectiveDate)
                .IsRequired();
                
            builder.Property(r => r.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(r => r.Notes)
                .HasMaxLength(500);
                
            builder.HasIndex(r => new { r.ContractId, r.LCATId, r.EffectiveDate });
                
            builder.HasOne(r => r.Contract)
                .WithMany(c => c.LCATRates)
                .HasForeignKey(r => r.ContractId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasOne(r => r.LCAT)
                .WithMany(l => l.ContractRates)
                .HasForeignKey(r => r.LCATId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}