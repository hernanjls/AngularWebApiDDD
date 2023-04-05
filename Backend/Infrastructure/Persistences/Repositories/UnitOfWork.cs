using Microsoft.Extensions.Configuration;

using TEST.Infrastructure.Persistences.Contexts;
using TEST.Infrastructure.Persistences.Interfaces;

namespace TEST.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskContext _context;
        public ITaskRepository Task { get; private set; }
       

        public UnitOfWork(TaskContext context, IConfiguration configuration)
        {
            _context = context;
            Task = new TaskRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}