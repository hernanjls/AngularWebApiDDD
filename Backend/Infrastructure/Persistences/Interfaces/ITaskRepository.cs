using TEST.Domain.Entities;
using TEST.Infrastructure.Commons.Bases.Request;
using TEST.Infrastructure.Commons.Bases.Response;
using TaskEntity = TEST.Domain.Entities.TaskEntity;

namespace TEST.Infrastructure.Persistences.Interfaces
{
    public interface ITaskRepository : IGenericRepository<Domain.Entities.TaskEntity>
    {
        Task<BaseEntityResponse<TaskEntity>> ListTasks(BaseFiltersRequest filters);
    }
}