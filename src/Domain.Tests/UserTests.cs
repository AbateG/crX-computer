using System;
using Xunit;
using CR_COMPUTER.Domain.Entities;
using CR_COMPUTER.Domain.Enums;

namespace CR_COMPUTER.Domain.Tests
{
    public class UserTests
    {
        [Fact]
        public void CreateUser_ValidData_ShouldCreateUser()
        {
            // Arrange
            var email = "test@example.com";
            var firstName = "John";
            var lastName = "Doe";
            var role = UserRole.FieldStaff;

            // Act
            var user = new User(email, firstName, lastName, role);

            // Assert
            Assert.Equal(email, user.Email);
            Assert.Equal(firstName, user.FirstName);
            Assert.Equal(lastName, user.LastName);
            Assert.Equal(role, user.Role);
            Assert.True(user.IsActive);
            Assert.NotEqual(Guid.Empty, user.Id);
            Assert.True(user.CreatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public void CreateUser_NullEmail_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new User(null!, "John", "Doe", UserRole.FieldStaff));
        }

        [Fact]
        public void GetFullName_ShouldReturnCorrectFormat()
        {
            // Arrange
            var user = new User("test@example.com", "John", "Doe", UserRole.FieldStaff);

            // Act
            var fullName = user.GetFullName();

            // Assert
            Assert.Equal("John Doe", fullName);
        }

        [Fact]
        public void Deactivate_ShouldSetIsActiveToFalse()
        {
            // Arrange
            var user = new User("test@example.com", "John", "Doe", UserRole.FieldStaff);

            // Act
            user.Deactivate();

            // Assert
            Assert.False(user.IsActive);
        }

        [Fact]
        public void UpdateLastLogin_ShouldUpdateLastLoginAt()
        {
            // Arrange
            var user = new User("test@example.com", "John", "Doe", UserRole.FieldStaff);
            var beforeUpdate = DateTime.UtcNow;

            // Act
            user.UpdateLastLogin();

            // Assert
            Assert.True(user.LastLoginAt >= beforeUpdate);
        }
    }
}
