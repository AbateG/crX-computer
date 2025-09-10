using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CR_COMPUTER.Domain.Entities;

namespace CR_COMPUTER.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configuration for TaskComment entity
    /// </summary>
    public class TaskCommentConfiguration : IEntityTypeConfiguration<TaskComment>
    {
        public void Configure(EntityTypeBuilder<TaskComment> builder)
        {
            builder.ToTable("TaskComments");

            builder.HasKey(tc => tc.Id);

            builder.Property(tc => tc.Content)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(tc => tc.CreatedAt)
                .IsRequired();

            // Indexes
            builder.HasIndex(tc => tc.TaskId);
            builder.HasIndex(tc => tc.CreatedById);

            // Relationships
            builder.HasOne(tc => tc.Task)
                .WithMany(t => t.Comments)
                .HasForeignKey(tc => tc.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tc => tc.CreatedBy)
                .WithMany()
                .HasForeignKey(tc => tc.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
