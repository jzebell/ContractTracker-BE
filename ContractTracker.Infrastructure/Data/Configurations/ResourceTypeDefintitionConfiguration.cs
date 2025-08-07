using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContractTracker.Domain.Entities;

namespace ContractTracker.Infrastructure.Data.Configurations
{
    public class ResourceTypeDefinitionConfiguration : IEntityTypeConfiguration<ResourceTypeDefinition>
    {
        public void Configure(EntityTypeBuilder<ResourceTypeDefinition> builder)
        {
            builder.ToTable("ResourceTypeDefinitions");
            builder.HasKey(r => r.Id);
            
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.HasIndex(r => r.Name)
                .IsUnique();
                
            builder.Property(r => r.Description)
                .HasMaxLength(500);
                
            builder.Property(r => r.DefaultWrapRate)
                .HasPrecision(5, 4)
                .IsRequired();
                
            builder.Property(r => r.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}