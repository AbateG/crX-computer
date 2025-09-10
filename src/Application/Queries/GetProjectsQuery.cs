using MediatR;
using CR_COMPUTER.Application.DTOs;

namespace CR_COMPUTER.Application.Queries
{
    /// <summary>
    /// Query to get all projects with pagination
    /// </summary>
    public class GetProjectsQuery : IRequest<PagedResult<ProjectDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public Domain.Enums.ProjectStatus? Status { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? ProjectManagerId { get; set; }
        public string? SortBy { get; set; } = "CreatedAt";
        public bool SortDescending { get; set; } = true;
    }
}
