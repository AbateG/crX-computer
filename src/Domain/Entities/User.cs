using System;
using CR_COMPUTER.Domain.ValueObjects;
using CR_COMPUTER.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace CR_COMPUTER.Domain.Entities
{
    /// <summary>
    /// Represents a user in the workflow management system
    /// </summary>
    public class User
    {
    public Guid Id { get; private set; }
    public string Email { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public Address? Address { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string PasswordHash { get; private set; } = null!;

        // Navigation properties
        private readonly List<Project> _assignedProjects = new();
        public IReadOnlyCollection<Project> AssignedProjects => _assignedProjects.AsReadOnly();

        private readonly List<Task> _assignedTasks = new();
        public IReadOnlyCollection<Task> AssignedTasks => _assignedTasks.AsReadOnly();

    protected User() { }

        public User(string email, string firstName, string lastName, UserRole role)
        {
            Id = Guid.NewGuid();
            Email = email ?? throw new ArgumentNullException(nameof(email));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Role = role;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, Microsoft.AspNetCore.Identity.IPasswordHasher<User> hasher)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.", nameof(password));
            PasswordHash = hasher.HashPassword(this, password);
        }

        public bool VerifyPassword(string password, Microsoft.AspNetCore.Identity.IPasswordHasher<User> hasher)
        {
            var result = hasher.VerifyHashedPassword(this, PasswordHash, password);
            return result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success;
        }

        public void UpdateContactInfo(string? phoneNumber, Address? address)
        {
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Reactivate()
        {
            IsActive = true;
        }

        public void UpdateLastLogin()
        {
            LastLoginAt = DateTime.UtcNow;
        }

        public string GetFullName() => $"{FirstName} {LastName}";
    }
}
