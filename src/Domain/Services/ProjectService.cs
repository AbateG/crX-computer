using System;
using CR_COMPUTER.Domain.Entities;
using CR_COMPUTER.Domain.ValueObjects;

namespace CR_COMPUTER.Domain.Services
{
    /// <summary>
    /// Domain service for project-related business logic
    /// </summary>
    public class ProjectService
    {
        /// <summary>
        /// Calculates the total cost of a project including all tasks and resources
        /// </summary>
        public decimal CalculateTotalProjectCost(Project project)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));

            decimal taskCosts = project.Tasks.Sum(t => t.EstimatedCost);
            decimal resourceCosts = project.Resources.Sum(r => r.Cost);

            return taskCosts + resourceCosts;
        }

        /// <summary>
        /// Determines if a project is on budget based on current spending
        /// </summary>
        public bool IsProjectOnBudget(Project project)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));

            decimal totalCost = CalculateTotalProjectCost(project);
            return totalCost <= project.Budget;
        }

        /// <summary>
        /// Calculates the progress percentage of a project
        /// </summary>
        public double CalculateProjectProgress(Project project)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));

            if (!project.Tasks.Any()) return 0;

            int completedTasks = project.Tasks.Count(t => t.Status == Enums.TaskStatus.Completed);
            return (double)completedTasks / project.Tasks.Count * 100;
        }

        /// <summary>
        /// Checks if a project is overdue
        /// </summary>
        public bool IsProjectOverdue(Project project)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));

            if (!project.DueDate.HasValue) return false;

            return project.DueDate.Value < DateTime.UtcNow && project.Status != Enums.ProjectStatus.Completed;
        }

        /// <summary>
        /// Gets the count of overdue tasks in a project
        /// </summary>
        public int GetOverdueTasksCount(Project project)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));

            return project.Tasks.Count(t => t.IsOverdue());
        }

        /// <summary>
        /// Validates if a user can be assigned as project manager
        /// </summary>
        public bool CanAssignAsProjectManager(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return user.Role == Enums.UserRole.Admin || user.Role == Enums.UserRole.Manager;
        }

        /// <summary>
        /// Gets the most critical task in a project based on priority and due date
        /// </summary>
    public CR_COMPUTER.Domain.Entities.Task? GetMostCriticalTask(Project project)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));

            return project.Tasks
                .Where(t => t.Status != CR_COMPUTER.Domain.Enums.TaskStatus.Completed && t.Status != CR_COMPUTER.Domain.Enums.TaskStatus.Cancelled)
                .OrderByDescending(t => (int)t.Priority)
                .ThenBy(t => t.DueDate ?? DateTime.MaxValue)
                .FirstOrDefault();
        }
    }
}
