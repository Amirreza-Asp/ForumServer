using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Forum.Infrastructure
{
    public static class Registrations
    {
        public static IServiceCollection AddInfrastructureRegistrations(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
