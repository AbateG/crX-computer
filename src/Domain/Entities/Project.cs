using System;
using CR_COMPUTER.Domain.ValueObjects;
using CR_COMPUTER.Domain.Enums;

namespace CR_COMPUTER.Domain.Entities
{
    /// <summary>
    /// Represents a project in the workflow management system
    /// </summary>
    public class Project
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ProjectStatus Status { get; private set; }
        public Priority Priority { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public DateTime? DueDate { get; private set; }
        public decimal Budget { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid? ProjectManagerId { get; private set; }
        public Address? Location { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Navigation properties
        public User? Client { get; private set; }
        public User? ProjectManager { get; private set; }

        private readonly List<Task> _tasks = new();
        public IReadOnlyCollection<Task> Tasks => _tasks.AsReadOnly();

        private readonly List<User> _assignedUsers = new();
        public IReadOnlyCollection<User> AssignedUsers => _assignedUsers.AsReadOnly();

        private readonly List<Resource> _resources = new();
        public IReadOnlyCollection<Resource> Resources => _resources.AsReadOnly();

        protected Project() { } // EF Core constructor

        public Project(string name, string description, Guid clientId, DateTime startDate, decimal budget)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Status = ProjectStatus.Planning;
            Priority = Priority.Medium;
            StartDate = startDate;
            Budget = budget;
            ClientId = clientId;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string name, string description, Priority priority, DateTime? dueDate, decimal budget)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Priority = priority;
            DueDate = dueDate;
            Budget = budget;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AssignProjectManager(Guid projectManagerId)
        {
            ProjectManagerId = projectManagerId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetLocation(Address location)
        {
            Location = location;
            UpdatedAt = DateTime.UtcNow;
        }

        public void StartProject()
        {
            if (Status != ProjectStatus.Planning)
                throw new InvalidOperationException("Can only start projects in planning status");

            Status = ProjectStatus.InProgress;
            UpdatedAt = DateTime.UtcNow;
        }

        public void CompleteProject()
        {
            if (Status != ProjectStatus.InProgress)
                throw new InvalidOperationException("Can only complete projects in progress");

            Status = ProjectStatus.Completed;
            EndDate = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void CancelProject()
        {
            if (Status == ProjectStatus.Completed)
                throw new InvalidOperationException("Cannot cancel completed projects");

            Status = ProjectStatus.Cancelled;
            EndDate = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public decimal GetTotalCost()
        {
            return _tasks.Sum(t => t.EstimatedCost) + _resources.Sum(r => r.Cost);
        }

        public int GetCompletedTasksCount()
        {
            return _tasks.Count(t => t.Status == CR_COMPUTER.Domain.Enums.TaskStatus.Completed);
        }

        public double GetProgressPercentage()
        {
            if (!_tasks.Any()) return 0;
            return (double)GetCompletedTasksCount() / _tasks.Count * 100;
        }
    }
}
