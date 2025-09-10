using System;

namespace CR_COMPUTER.Domain.Entities
{
    /// <summary>
    /// Represents a comment on a task
    /// </summary>
    public class TaskComment
    {
        public Guid Id { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid TaskId { get; private set; }
        public Guid CreatedById { get; private set; }

        // Navigation properties
        public Task Task { get; private set; } = null!;
        public User CreatedBy { get; private set; } = null!;

        protected TaskComment() { } // EF Core constructor

        public TaskComment(string content, Guid taskId, Guid createdById)
        {
            Id = Guid.NewGuid();
            Content = content ?? throw new ArgumentNullException(nameof(content));
            TaskId = taskId;
            CreatedById = createdById;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateContent(string content)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }
    }
}
