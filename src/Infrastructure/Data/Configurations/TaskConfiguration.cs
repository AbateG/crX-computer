using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CR_COMPUTER.Domain.Entities;
using CR_COMPUTER.Domain.Enums;

namespace CR_COMPUTER.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configuration for Task entity
    /// </summary>
    public class TaskConfiguration : IEntityTypeConfiguration<CR_COMPUTER.Domain.Entities.Task>
    {
        public void Configure(EntityTypeBuilder<CR_COMPUTER.Domain.Entities.Task> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(t => t.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(t => t.Priority)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(t => t.CreatedAt)
                .IsRequired();

            builder.Property(t => t.StartedAt)
                .IsRequired(false);

            builder.Property(t => t.CompletedAt)
                .IsRequired(false);

            builder.Property(t => t.DueDate)
                .IsRequired(false);

            builder.Property(t => t.EstimatedCost)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.ActualCost)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            // Indexes
            builder.HasIndex(t => t.ProjectId);
            builder.HasIndex(t => t.AssignedUserId);
            builder.HasIndex(t => t.CreatedById);
            builder.HasIndex(t => t.Status);
            builder.HasIndex(t => t.Priority);
            builder.HasIndex(t => t.DueDate);

            // Relationships
            builder.HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.AssignedUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.CreatedBy)
                .WithMany()
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.Comments)
                .WithOne(c => c.Task)
                .HasForeignKey(c => c.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Attachments)
                .WithOne(a => a.Task)
                .HasForeignKey(a => a.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
