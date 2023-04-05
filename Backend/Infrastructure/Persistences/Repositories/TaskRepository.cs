using Microsoft.EntityFrameworkCore;
using TEST.Domain.Entities;
using TEST.Infrastructure.Commons.Bases.Request;
using TEST.Infrastructure.Commons.Bases.Response;
using TEST.Infrastructure.Persistences.Contexts;
using TEST.Infrastructure.Persistences.Interfaces;
using TaskEntity = TEST.Domain.Entities.TaskEntity;

namespace TEST.Infrastructure.Persistences.Repositories
{
    public class TaskRepository : GenericRepository<Domain.Entities.TaskEntity>, ITaskRepository
    {
        public TaskRepository(TaskContext context) : base(context) { }

        public async Task<BaseEntityResponse<TaskEntity>> ListTasks(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Domain.Entities.TaskEntity>();

            var tasks = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        tasks = tasks.Where(x => x.Name!.Contains(filters.TextFilter));
                        break;
                    case 2:
                        tasks = tasks.Where(x => x.Description!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                tasks = tasks.Where(x => x.State.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                tasks = tasks.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            if (filters.Sort is null) filters.Sort = "Id";

            response.TotalRecords = await tasks.CountAsync();
            response.Items = await Ordering(filters, tasks, !(bool)filters.Download!).ToListAsync();
            return response;
        }
    }
}