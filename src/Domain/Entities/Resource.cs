using System;
using CR_COMPUTER.Domain.ValueObjects;
using CR_COMPUTER.Domain.Enums;

namespace CR_COMPUTER.Domain.Entities
{
    /// <summary>
    /// Represents a resource that can be allocated to projects
    /// </summary>
    public class Resource
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ResourceType Type { get; private set; }
        public decimal Cost { get; private set; }
        public string Unit { get; private set; } // e.g., "hours", "pieces", "square feet"
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Navigation properties
        private readonly List<Project> _projects = new();
        public IReadOnlyCollection<Project> Projects => _projects.AsReadOnly();

        protected Resource() { } // EF Core constructor

        public Resource(string name, string description, ResourceType type, decimal cost, string unit)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Type = type;
            Cost = cost;
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string name, string description, decimal cost, string unit)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Cost = cost;
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Reactivate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
