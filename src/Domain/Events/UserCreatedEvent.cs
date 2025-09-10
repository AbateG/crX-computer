using System;

namespace CR_COMPUTER.Domain.Events
{
    /// <summary>
    /// Event raised when a new user is created
    /// </summary>
    public class UserCreatedEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string FullName { get; }
        public Enums.UserRole Role { get; }

        public UserCreatedEvent(Guid userId, string email, string fullName, Enums.UserRole role)
        {
            UserId = userId;
            Email = email;
            FullName = fullName;
            Role = role;
        }
    }
}
