using Forum.Application.Repositories;
using Forum.Application.Repositories.Communications;
using Forum.Application.Services;
using Forum.Persistence.Reposiotories;
using Forum.Persistence.Reposiotories.Communications;
using Forum.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Forum.Persistence
{
    public static class Registration
    {
        public static IServiceCollection AddPersistenceRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                //options.UseInMemoryDatabase("Forum");
            });

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITopicRepository, TopicRepository>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
