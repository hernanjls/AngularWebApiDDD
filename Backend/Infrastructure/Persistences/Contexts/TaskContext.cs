using Microsoft.EntityFrameworkCore;
using TEST.Domain.Entities;
using System.Reflection;

namespace TEST.Infrastructure.Persistences.Contexts
{
    public partial class TaskContext : DbContext
    {
        public TaskContext()
        {
        }

        public TaskContext(DbContextOptions<TaskContext> options)
            : base(options)
        {
        }

       
        public virtual DbSet<Domain.Entities.TaskEntity> Tasks { get; set; } = null!;
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}