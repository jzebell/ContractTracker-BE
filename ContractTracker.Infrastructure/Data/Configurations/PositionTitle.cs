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

            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.LCATId)
                .IsRequired();

            builder.Property(pt => pt.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(pt => pt.IsActive)
                .IsRequired();

            builder.Property(pt => pt.CreatedDate)
                .IsRequired();

            builder.Property(pt => pt.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(pt => pt.LCAT)
                .WithMany(l => l.PositionTitles)
                .HasForeignKey(pt => pt.LCATId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(pt => new { pt.LCATId, pt.Title })
                .HasDatabaseName("IX_PositionTitles_LCAT_Title");
        }
    }
}