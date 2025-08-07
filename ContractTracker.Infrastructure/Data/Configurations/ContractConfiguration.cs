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
                .HasMaxLength(100);
                
            builder.HasIndex(c => c.ContractNumber)
                .IsUnique();
                
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(300);
                
            builder.Property(c => c.ProgramName)
                .HasMaxLength(300);
                
            builder.Property(c => c.TotalValue)
                .HasPrecision(18, 2)
                .IsRequired();
                
            builder.Property(c => c.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.HasMany(c => c.LCATRates)
                .WithOne(lr => lr.Contract)
                .HasForeignKey(lr => lr.ContractId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasMany(c => c.Resources)
                .WithOne(r => r.Contract)
                .HasForeignKey(r => r.ContractId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}