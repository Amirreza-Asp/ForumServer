using Forum.Application.Repositories;
using Forum.Application.Repositories.Communications;
using Forum.Application.Repositories.Logs;
using Forum.Application.Services;
using Forum.Persistence.Reposiotories;
using Forum.Persistence.Reposiotories.Communications;
using Forum.Persistence.Reposiotories.Logs;
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

            // SqlServer
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                //options.UseInMemoryDatabase("Forum");
            });

            // MongoDb
            services.AddSingleton<LogDbContext>();

            // Redis 
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<String>("CacheSettings:ConnectionString");
            });

            // Initializer
            services.AddScoped<IDbInitializer, DbInitializer>();

            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ICommunityRepository, CommunityRepository>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
