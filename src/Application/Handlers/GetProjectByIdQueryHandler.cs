using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CR_COMPUTER.Application.Queries;
using CR_COMPUTER.Application.DTOs;
using CR_COMPUTER.Domain.Interfaces;
using CR_COMPUTER.Domain.Services;

namespace CR_COMPUTER.Application.Handlers
{
    /// <summary>
    /// Handler for GetProjectByIdQuery
    /// </summary>
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public GetProjectByIdQueryHandler(
            IProjectRepository projectRepository,
            IUserRepository userRepository)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ProjectDto?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (project == null) return null;

            var projectService = new ProjectService();

            // Get client and project manager names
            var client = await _userRepository.GetByIdAsync(project.ClientId);
            var projectManager = project.ProjectManagerId.HasValue
                ? await _userRepository.GetByIdAsync(project.ProjectManagerId.Value)
                : null;

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Status = project.Status,
                Priority = project.Priority,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                DueDate = project.DueDate,
                Budget = project.Budget,
                ClientId = project.ClientId,
                ClientName = client?.GetFullName(),
                ProjectManagerId = project.ProjectManagerId,
                ProjectManagerName = projectManager?.GetFullName(),
                Location = project.Location?.ToString(),
                CreatedAt = project.CreatedAt,
                TotalCost = projectService.CalculateTotalProjectCost(project),
                ProgressPercentage = projectService.CalculateProjectProgress(project),
                TotalTasks = project.Tasks.Count,
                CompletedTasks = project.GetCompletedTasksCount(),
                IsOverdue = projectService.IsProjectOverdue(project)
            };
        }
    }
}
