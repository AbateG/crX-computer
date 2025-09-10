using System;

namespace CR_COMPUTER.Domain.Events
{
    /// <summary>
    /// Event raised when a new project is created
    /// </summary>
    public class ProjectCreatedEvent : DomainEvent
    {
        public Guid ProjectId { get; }
        public string ProjectName { get; }
        public Guid ClientId { get; }
        public Guid? ProjectManagerId { get; }

        public ProjectCreatedEvent(Guid projectId, string projectName, Guid clientId, Guid? projectManagerId)
        {
            ProjectId = projectId;
            ProjectName = projectName;
            ClientId = clientId;
            ProjectManagerId = projectManagerId;
        }
    }
}
