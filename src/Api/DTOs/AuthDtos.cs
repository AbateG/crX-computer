using System;
using System.ComponentModel.DataAnnotations;
using CR_COMPUTER.Domain.Enums;

namespace CR_COMPUTER.Api.DTOs
{
    /// <summary>
    /// Request DTO for user login
    /// </summary>
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = null!;
    }

    /// <summary>
    /// Request DTO for user registration
    /// </summary>
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        public UserRole Role { get; set; }
    }

    /// <summary>
    /// Response DTO for authentication
    /// </summary>
    public class AuthResponse
    {
        public string Token { get; set; } = null!;
        public UserDto User { get; set; } = null!;
    }

    /// <summary>
    /// User DTO
    /// </summary>
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
    }
}
