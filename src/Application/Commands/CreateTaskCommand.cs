using System;
using MediatR;

namespace CR_COMPUTER.Application.Commands
{
    /// <summary>
    /// Command to create a new task
    /// </summary>
    public class CreateTaskCommand : IRequest<Guid>
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public Guid CreatedById { get; set; }
        public Domain.Enums.Priority Priority { get; set; } = Domain.Enums.Priority.Medium;
        public DateTime? DueDate { get; set; }
        public decimal EstimatedCost { get; set; }
        public TimeSpan? EstimatedDuration { get; set; }
    }
}
