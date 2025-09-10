using System;
using MediatR;

namespace CR_COMPUTER.Application.Commands
{
    /// <summary>
    /// Command to update an existing project
    /// </summary>
    public class UpdateProjectCommand : IRequest
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Domain.Enums.Priority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal Budget { get; set; }
    }
}
