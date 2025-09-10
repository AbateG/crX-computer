using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CR_COMPUTER.Application.Commands;
using CR_COMPUTER.Domain.Entities;
using CR_COMPUTER.Domain.Events;
using CR_COMPUTER.Domain.Services;
using CR_COMPUTER.Domain.Interfaces;

namespace CR_COMPUTER.Application.Handlers
{
    /// <summary>
    /// Handler for CreateProjectCommand
    /// </summary>
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public CreateProjectCommandHandler(
            IProjectRepository projectRepository,
            IUserRepository userRepository,
            IMediator mediator)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            // Validate that client exists
            var client = await _userRepository.GetByIdAsync(request.ClientId);
            if (client == null)
                throw new InvalidOperationException("Client not found");

            // Validate project manager if provided
            if (request.ProjectManagerId.HasValue)
            {
                var projectManager = await _userRepository.GetByIdAsync(request.ProjectManagerId.Value);
                if (projectManager == null)
                    throw new InvalidOperationException("Project manager not found");

                var projectService = new ProjectService();
                if (!projectService.CanAssignAsProjectManager(projectManager))
                    throw new InvalidOperationException("User cannot be assigned as project manager");
            }

            // Create the project
            var project = new Project(
                request.Name,
                request.Description,
                request.ClientId,
                request.StartDate,
                request.Budget);

            if (request.ProjectManagerId.HasValue)
                project.AssignProjectManager(request.ProjectManagerId.Value);

            // Save to repository
            await _projectRepository.AddAsync(project);

            // Publish domain event
            await _mediator.Publish(new ProjectCreatedEvent(
                project.Id,
                project.Name,
                project.ClientId,
                project.ProjectManagerId), cancellationToken);

            return project.Id;
        }
    }
}
