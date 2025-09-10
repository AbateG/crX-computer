using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CR_COMPUTER.Domain.Entities;
using CR_COMPUTER.Domain.Interfaces;
using CR_COMPUTER.Infrastructure.Data;

namespace CR_COMPUTER.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for Project entity
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.ProjectManager)
                .Include(p => p.Tasks)
                .Include(p => p.Resources)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

    public async System.Threading.Tasks.Task AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

    public async System.Threading.Tasks.Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

    public async System.Threading.Tasks.Task DeleteAsync(Guid id)
        {
            var project = await GetByIdAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.ProjectManager)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetByClientIdAsync(Guid clientId)
        {
            return await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.ProjectManager)
                .Where(p => p.ClientId == clientId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetByProjectManagerIdAsync(Guid projectManagerId)
        {
            return await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.ProjectManager)
                .Where(p => p.ProjectManagerId == projectManagerId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}
