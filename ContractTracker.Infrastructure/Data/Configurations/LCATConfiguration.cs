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

            builder.Property(l => l.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(l => l.Description)
                .HasMaxLength(500);

            builder.Property(l => l.Category)
                .HasMaxLength(100);

            builder.Property(l => l.IsActive)
                .IsRequired();

            builder.Property(l => l.CreatedAt)
                .IsRequired();

            builder.Property(l => l.UpdatedAt)
                .IsRequired();

            builder.Property(l => l.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.ModifiedBy)
                .IsRequired()
                .HasMaxLength(100);

            // Relationships
            builder.HasMany(l => l.Rates)
                .WithOne(r => r.LCAT)
                .HasForeignKey(r => r.LCATId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(l => l.PositionTitles)
                .WithOne(pt => pt.LCAT)
                .HasForeignKey(pt => pt.LCATId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(l => l.Code)
                .IsUnique();

            builder.HasIndex(l => l.IsActive)
                .HasDatabaseName("IX_LCATs_IsActive");
        }
    }
}