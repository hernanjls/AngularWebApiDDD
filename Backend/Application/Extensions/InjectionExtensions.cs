using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TEST.Application.Interfaces;
using TEST.Application.Services;
using System.Reflection;

namespace TEST.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ITaskApplication, TaskApplication>();
  

            return services;
        }
    }
}