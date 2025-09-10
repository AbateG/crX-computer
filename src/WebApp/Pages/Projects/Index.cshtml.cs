using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using CR_COMPUTER.Application.Queries;
using CR_COMPUTER.Application.DTOs;

namespace CR_COMPUTER.WebApp.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public PagedResult<ProjectDto>? Projects { get; set; }

        public async Task<IActionResult> OnGetAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            Domain.Enums.ProjectStatus? status = null,
            Guid? clientId = null,
            Guid? projectManagerId = null,
            string? sortBy = "CreatedAt",
            bool sortDescending = true)
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

            Projects = await _mediator.Send(query);

            return Page();
        }
    }
}
