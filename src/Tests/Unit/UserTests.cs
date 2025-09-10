using Xunit;
using CR_COMPUTER.Domain.Entities;
using System;

namespace Tests.Unit
{
    public class UserTests
    {
        [Fact]
        public void CreateUser_ValidData_ShouldCreateUser()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Test User";
            var email = "test@example.com";

            // Act
            var user = new User(id, name, email);

            // Assert
            Assert.Equal(id, user.Id);
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
        }
    }
}
