using Microsoft.EntityFrameworkCore;
using CR_COMPUTER.Domain.Entities;
using CR_COMPUTER.Domain.ValueObjects;
using CR_COMPUTER.Infrastructure.Data.Configurations;

namespace CR_COMPUTER.Infrastructure.Data
{
    /// <summary>
    /// Application DbContext for Entity Framework Core
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for entities
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<CR_COMPUTER.Domain.Entities.Task> Tasks { get; set; } = null!;
        public DbSet<TaskComment> TaskComments { get; set; } = null!;
        public DbSet<TaskAttachment> TaskAttachments { get; set; } = null!;
        public DbSet<Resource> Resources { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new TaskCommentConfiguration());
            modelBuilder.ApplyConfiguration(new TaskAttachmentConfiguration());
            modelBuilder.ApplyConfiguration(new ResourceConfiguration());

            // Configure value objects
            modelBuilder.Owned<Address>();
            modelBuilder.Owned<Money>();
            modelBuilder.Owned<DateRange>();
        }
    }
}
