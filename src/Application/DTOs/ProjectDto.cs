using System;
using CR_COMPUTER.Domain.Enums;

namespace CR_COMPUTER.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for Project
    /// </summary>
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ProjectStatus Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal Budget { get; set; }
        public Guid ClientId { get; set; }
        public string? ClientName { get; set; }
        public Guid? ProjectManagerId { get; set; }
        public string? ProjectManagerName { get; set; }
        public string? Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalCost { get; set; }
        public double ProgressPercentage { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public bool IsOverdue { get; set; }
    }
}
