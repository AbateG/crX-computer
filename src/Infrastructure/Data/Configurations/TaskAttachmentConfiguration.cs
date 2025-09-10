using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CR_COMPUTER.Domain.Entities;

namespace CR_COMPUTER.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configuration for TaskAttachment entity
    /// </summary>
    public class TaskAttachmentConfiguration : IEntityTypeConfiguration<TaskAttachment>
    {
        public void Configure(EntityTypeBuilder<TaskAttachment> builder)
        {
            builder.ToTable("TaskAttachments");

            builder.HasKey(ta => ta.Id);

            builder.Property(ta => ta.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(ta => ta.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(ta => ta.ContentType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ta => ta.FileSize)
                .IsRequired();

            builder.Property(ta => ta.UploadedAt)
                .IsRequired();

            // Indexes
            builder.HasIndex(ta => ta.TaskId);
            builder.HasIndex(ta => ta.UploadedById);

            // Relationships
            builder.HasOne(ta => ta.Task)
                .WithMany(t => t.Attachments)
                .HasForeignKey(ta => ta.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ta => ta.UploadedBy)
                .WithMany()
                .HasForeignKey(ta => ta.UploadedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
