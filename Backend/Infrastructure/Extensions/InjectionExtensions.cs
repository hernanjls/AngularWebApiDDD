using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TEST.Infrastructure.Persistences.Contexts;
using TEST.Infrastructure.Persistences.Interfaces;
using TEST.Infrastructure.Persistences.Repositories;

namespace TEST.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(TaskContext).Assembly.FullName;

            services.AddDbContext<TaskContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("TaskConnection"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            

            return services;
        }
    }
}