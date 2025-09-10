using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CR_COMPUTER.Domain.Entities;
using CR_COMPUTER.Domain.Enums;

namespace CR_COMPUTER.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configuration for Resource entity
    /// </summary>
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("Resources");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(r => r.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(r => r.Cost)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(r => r.Unit)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.Property(r => r.UpdatedAt)
                .IsRequired(false);

            // Indexes
            builder.HasIndex(r => r.Type);
            builder.HasIndex(r => r.IsActive);

            // Relationships
            builder.HasMany(r => r.Projects)
                .WithMany(p => p.Resources)
                .UsingEntity(j => j.ToTable("ProjectResources"));
        }
    }
}
