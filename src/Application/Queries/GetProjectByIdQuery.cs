using System;
using MediatR;
using CR_COMPUTER.Application.DTOs;

namespace CR_COMPUTER.Application.Queries
{
    /// <summary>
    /// Query to get a project by ID
    /// </summary>
    public class GetProjectByIdQuery : IRequest<ProjectDto?>
    {
        public Guid ProjectId { get; set; }
    }
}
