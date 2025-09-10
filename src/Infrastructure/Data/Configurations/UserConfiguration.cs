using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CR_COMPUTER.Domain.Entities;
using CR_COMPUTER.Domain.Enums;

namespace CR_COMPUTER.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configuration for User entity
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.LastLoginAt)
                .IsRequired(false);

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(20);

            // Indexes
            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasIndex(u => u.Role);

            // Relationships
            builder.HasMany(u => u.AssignedProjects)
                .WithOne(p => p.ProjectManager!)
                .HasForeignKey("ProjectManagerId")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.AssignedTasks)
                .WithOne(t => t.AssignedUser!)
                .HasForeignKey("AssignedUserId")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
