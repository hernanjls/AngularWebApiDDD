
namespace TEST.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //Declaración o matricula de nuestra interfaces a nivel de repository
        ITaskRepository Task { get; }
      
        void SaveChanges();
        Task SaveChangesAsync();
    }
}