using System;
using MediatR;

namespace CR_COMPUTER.Application.Commands
{
    /// <summary>
    /// Command to create a new project
    /// </summary>
    public class CreateProjectCommand : IRequest<Guid>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Budget { get; set; }
        public Guid? ProjectManagerId { get; set; }
    }
}
