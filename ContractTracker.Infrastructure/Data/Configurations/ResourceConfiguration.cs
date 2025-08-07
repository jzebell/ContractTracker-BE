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
                .HasMaxLength(255);
                
            builder.HasIndex(r => r.Email)
                .IsUnique();
                
            builder.Property(r => r.ResourceType)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();
                
            builder.Property(r => r.HourlyRate)
                .HasPrecision(10, 2)
                .IsRequired();
                
            builder.Property(r => r.AnnualSalary)
                .HasPrecision(12, 2);
                
            builder.Property(r => r.FixedPriceAmount)
                .HasPrecision(12, 2);
                
            builder.Property(r => r.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Ignore(r => r.FullName);
                
            builder.HasOne(r => r.LCAT)
                .WithMany()
                .HasForeignKey(r => r.LCATId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne(r => r.Contract)
                .WithMany(c => c.Resources)
                .HasForeignKey(r => r.ContractId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}