using Microsoft.EntityFrameworkCore;

namespace TasksApi.Models // ✅ necessário para funcionar com as migrations
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        public DbSet<TaskModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskModel>()
                .Property(t => t.Status)
                .HasConversion<string>();
        }
    }


}
