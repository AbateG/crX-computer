using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CR_COMPUTER.Domain.Entities;
using CR_COMPUTER.Domain.Enums;

namespace CR_COMPUTER.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configuration for Project entity
    /// </summary>
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            // Ignore navigation property not mapped to a relationship
            builder.Ignore(p => p.AssignedUsers);
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(p => p.Priority)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(p => p.StartDate)
                .IsRequired();

            builder.Property(p => p.EndDate)
                .IsRequired(false);

            builder.Property(p => p.DueDate)
                .IsRequired(false);

            builder.Property(p => p.Budget)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.UpdatedAt)
                .IsRequired(false);

            // Indexes
            builder.HasIndex(p => p.ClientId);
            builder.HasIndex(p => p.ProjectManagerId);
            builder.HasIndex(p => p.Status);
            builder.HasIndex(p => p.Priority);
            builder.HasIndex(p => p.StartDate);
            builder.HasIndex(p => p.DueDate);

            // Relationships
            builder.HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ProjectManager)
                .WithMany(u => u.AssignedProjects)
                .HasForeignKey(p => p.ProjectManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Resources)
                .WithMany(r => r.Projects)
                .UsingEntity(j => j.ToTable("ProjectResources"));
        }
    }
}
