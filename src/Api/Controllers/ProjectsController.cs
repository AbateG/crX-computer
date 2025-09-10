using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using CR_COMPUTER.Application.Commands;
using CR_COMPUTER.Application.Queries;
using CR_COMPUTER.Application.DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace CR_COMPUTER.Api.Controllers
{
    /// <summary>
    /// API controller for project operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;

        public ProjectsController(IMediator mediator, Microsoft.Extensions.Caching.Memory.IMemoryCache cache)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        /// <summary>
        /// Get all projects with pagination
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<ProjectDto>), 200)]
        public async Task<IActionResult> GetProjects(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] Domain.Enums.ProjectStatus? status = null,
            [FromQuery] Guid? clientId = null,
            [FromQuery] Guid? projectManagerId = null,
            [FromQuery] string? sortBy = "CreatedAt",
            [FromQuery] bool sortDescending = true)
        {
            var cacheKey = $"projects_{pageNumber}_{pageSize}_{searchTerm}_{status}_{clientId}_{projectManagerId}_{sortBy}_{sortDescending}";
            PagedResult<ProjectDto>? result = null;
            if (!_cache.TryGetValue(cacheKey, out object cachedObj) || cachedObj is not PagedResult<ProjectDto> cachedResult)
            {
                var query = new GetProjectsQuery
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    SearchTerm = searchTerm,
                    Status = status,
                    ClientId = clientId,
                    ProjectManagerId = projectManagerId,
                    SortBy = sortBy,
                    SortDescending = sortDescending
                };
                result = await _mediator.Send(query);
                _cache.Set(cacheKey, result, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2) });
            }
            else
            {
                result = cachedResult;
            }
            return Ok(result);
        }

        /// <summary>
        /// Get project by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProject(Guid id)
        {
            var query = new GetProjectByIdQuery { ProjectId = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Create a new project
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command)
        {
            var projectId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProject), new { id = projectId }, projectId);
        }

        /// <summary>
        /// Update an existing project
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] UpdateProjectCommand command)
        {
            command.ProjectId = id;
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete a project
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            // TODO: Implement delete command
            return await Task.FromResult(NoContent());
        }
    }
}
