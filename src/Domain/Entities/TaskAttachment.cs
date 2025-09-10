using System;

namespace CR_COMPUTER.Domain.Entities
{
    /// <summary>
    /// Represents an attachment to a task
    /// </summary>
    public class TaskAttachment
    {
        public Guid Id { get; private set; }
        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public string ContentType { get; private set; }
        public long FileSize { get; private set; }
        public DateTime UploadedAt { get; private set; }
        public Guid TaskId { get; private set; }
        public Guid UploadedById { get; private set; }

        // Navigation properties
        public Task Task { get; private set; } = null!;
        public User UploadedBy { get; private set; } = null!;

        protected TaskAttachment() { } // EF Core constructor

        public TaskAttachment(string fileName, string filePath, string contentType, long fileSize, Guid taskId, Guid uploadedById)
        {
            Id = Guid.NewGuid();
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
            FileSize = fileSize;
            TaskId = taskId;
            UploadedById = uploadedById;
            UploadedAt = DateTime.UtcNow;
        }
    }
}
