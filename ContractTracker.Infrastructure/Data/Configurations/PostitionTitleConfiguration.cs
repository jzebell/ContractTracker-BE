using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContractTracker.Domain.Entities;

namespace ContractTracker.Infrastructure.Data.Configurations
{
    public class PositionTitleConfiguration : IEntityTypeConfiguration<PositionTitle>
    {
        public void Configure(EntityTypeBuilder<PositionTitle> builder)
        {
            builder.ToTable("PositionTitles");
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);
                
            builder.Property(p => p.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.HasIndex(p => new { p.Title, p.IsActive });
                
            builder.HasOne(p => p.LCAT)
                .WithMany(l => l.PositionTitles)
                .HasForeignKey(p => p.LCATId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}