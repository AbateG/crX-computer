using System;

namespace CR_COMPUTER.Domain.Events
{
    /// <summary>
    /// Event raised when a task is completed
    /// </summary>
    public class TaskCompletedEvent : DomainEvent
    {
        public Guid TaskId { get; }
        public Guid ProjectId { get; }
        public Guid CompletedById { get; }
        public DateTime CompletedAt { get; }

        public TaskCompletedEvent(Guid taskId, Guid projectId, Guid completedById, DateTime completedAt)
        {
            TaskId = taskId;
            ProjectId = projectId;
            CompletedById = completedById;
            CompletedAt = completedAt;
        }
    }
}
