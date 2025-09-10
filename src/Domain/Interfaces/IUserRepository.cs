using System;
using System.Threading.Tasks;
using CR_COMPUTER.Domain.Entities;

namespace CR_COMPUTER.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for User entity
    /// </summary>
    public interface IUserRepository
    {
    System.Threading.Tasks.Task<User?> GetByIdAsync(Guid id);
    System.Threading.Tasks.Task<User?> GetByEmailAsync(string email);
    System.Threading.Tasks.Task AddAsync(User user);
    System.Threading.Tasks.Task UpdateAsync(User user);
    System.Threading.Tasks.Task DeleteAsync(Guid id);
    System.Threading.Tasks.Task<IEnumerable<User>> GetAllAsync();
    System.Threading.Tasks.Task<IEnumerable<User>> GetByRoleAsync(Enums.UserRole role);
    System.Threading.Tasks.Task<bool> ExistsAsync(Guid id);
    }
}
