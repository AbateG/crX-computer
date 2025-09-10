using System;
using System.Threading.Tasks;
using CR_COMPUTER.Domain.Entities;

namespace CR_COMPUTER.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for Project entity
    /// </summary>
    public interface IProjectRepository
    {
    System.Threading.Tasks.Task<Project?> GetByIdAsync(Guid id);
    System.Threading.Tasks.Task AddAsync(Project project);
    System.Threading.Tasks.Task UpdateAsync(Project project);
    System.Threading.Tasks.Task DeleteAsync(Guid id);
    System.Threading.Tasks.Task<IEnumerable<Project>> GetAllAsync();
    System.Threading.Tasks.Task<IEnumerable<Project>> GetByClientIdAsync(Guid clientId);
    System.Threading.Tasks.Task<IEnumerable<Project>> GetByProjectManagerIdAsync(Guid projectManagerId);
    }
}
