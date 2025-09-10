using System;
using System.ComponentModel.DataAnnotations;
using CR_COMPUTER.Domain.Enums;

namespace CR_COMPUTER.Api.DTOs
{
    /// <summary>
    /// Request DTO for creating a project
    /// </summary>
    public class CreateProjectRequest
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = null!;

        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Budget { get; set; }

        public Guid? ProjectManagerId { get; set; }
    }

    /// <summary>
    /// Request DTO for updating a project
    /// </summary>
    public class UpdateProjectRequest
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = null!;

        [Required]
        public Priority Priority { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Budget { get; set; }
    }

    /// <summary>
    /// Response DTO for project operations
    /// </summary>
    public class ProjectResponse
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
