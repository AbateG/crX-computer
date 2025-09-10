using System;
using CR_COMPUTER.Domain.ValueObjects;
using CR_COMPUTER.Domain.Enums;

namespace CR_COMPUTER.Domain.Entities
{
    /// <summary>
    /// Represents a task within a project
    /// </summary>
    public class Task
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
    public CR_COMPUTER.Domain.Enums.TaskStatus Status { get; private set; }
        public Priority Priority { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public DateTime? DueDate { get; private set; }
        public decimal EstimatedCost { get; private set; }
        public decimal ActualCost { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid? AssignedUserId { get; private set; }
        public Guid? CreatedById { get; private set; }
        public TimeSpan? EstimatedDuration { get; private set; }
        public TimeSpan? ActualDuration { get; private set; }

        // Navigation properties
        public Project Project { get; private set; } = null!;
        public User? AssignedUser { get; private set; }
        public User? CreatedBy { get; private set; }

        private readonly List<TaskComment> _comments = new();
        public IReadOnlyCollection<TaskComment> Comments => _comments.AsReadOnly();

        private readonly List<TaskAttachment> _attachments = new();
        public IReadOnlyCollection<TaskAttachment> Attachments => _attachments.AsReadOnly();

        protected Task() { } // EF Core constructor

        public Task(string title, string description, Guid projectId, Guid createdById)
        {
            Id = Guid.NewGuid();
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Status = CR_COMPUTER.Domain.Enums.TaskStatus.NotStarted;
            Priority = Priority.Medium;
            ProjectId = projectId;
            CreatedById = createdById;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string title, string description, Priority priority, DateTime? dueDate, decimal estimatedCost, TimeSpan? estimatedDuration)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Priority = priority;
            DueDate = dueDate;
            EstimatedCost = estimatedCost;
            EstimatedDuration = estimatedDuration;
        }

        public void AssignToUser(Guid userId)
        {
            AssignedUserId = userId;
        }

        public void StartTask()
        {
            if (Status != CR_COMPUTER.Domain.Enums.TaskStatus.NotStarted && Status != CR_COMPUTER.Domain.Enums.TaskStatus.OnHold)
                throw new InvalidOperationException("Can only start tasks that are not started or on hold");

            Status = CR_COMPUTER.Domain.Enums.TaskStatus.InProgress;
            StartedAt = DateTime.UtcNow;
        }

        public void CompleteTask(decimal actualCost, TimeSpan? actualDuration)
        {
            if (Status != CR_COMPUTER.Domain.Enums.TaskStatus.InProgress)
                throw new InvalidOperationException("Can only complete tasks that are in progress");

            Status = CR_COMPUTER.Domain.Enums.TaskStatus.Completed;
            CompletedAt = DateTime.UtcNow;
            ActualCost = actualCost;
            ActualDuration = actualDuration;
        }

        public void PutOnHold()
        {
            if (Status != CR_COMPUTER.Domain.Enums.TaskStatus.InProgress)
                throw new InvalidOperationException("Can only put tasks on hold that are in progress");

            Status = CR_COMPUTER.Domain.Enums.TaskStatus.OnHold;
        }

        public void CancelTask()
        {
            if (Status == CR_COMPUTER.Domain.Enums.TaskStatus.Completed)
                throw new InvalidOperationException("Cannot cancel completed tasks");

            Status = CR_COMPUTER.Domain.Enums.TaskStatus.Cancelled;
        }

        public bool IsOverdue()
        {
            return DueDate.HasValue && DueDate.Value < DateTime.UtcNow && Status != CR_COMPUTER.Domain.Enums.TaskStatus.Completed;
        }

        public TimeSpan? GetTimeSpent()
        {
            if (!StartedAt.HasValue) return null;
            var endTime = CompletedAt ?? DateTime.UtcNow;
            return endTime - StartedAt.Value;
        }
    }
}
