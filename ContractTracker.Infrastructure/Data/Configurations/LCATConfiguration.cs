using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContractTracker.Domain.Entities;

namespace ContractTracker.Infrastructure.Data.Configurations
{
    public class LCATConfiguration : IEntityTypeConfiguration<LCAT>
    {
        public void Configure(EntityTypeBuilder<LCAT> builder)
        {
            builder.ToTable("LCATs");
            builder.HasKey(l => l.Id);
            
            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(200);
                
            builder.HasIndex(l => l.Name)
                .IsUnique();
                
            builder.Property(l => l.Description)
                .HasMaxLength(500);
                
            builder.Property(l => l.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(l => l.ModifiedBy)
                .HasMaxLength(100)
                .IsRequired(false);  // Make this nullable
                
            // Relationships
            builder.HasMany(l => l.Rates)
                .WithOne(r => r.LCAT)
                .HasForeignKey(r => r.LCATId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasMany(l => l.PositionTitles)
                .WithOne(p => p.LCAT)
                .HasForeignKey(p => p.LCATId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasMany(l => l.ContractRates)
                .WithOne(cr => cr.LCAT)
                .HasForeignKey(cr => cr.LCATId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}