using AutoMapper;
using Forum.Application.Services;
using Forum.Domain.ServicesModels;
using Forum.Infrastructure.Mappings;
using Forum.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;

namespace Forum.Infrastructure
{
    public static class Registrations
    {
        private static readonly String _modelFile =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../wwwroot/AggressionScore", "AggressionScoreRetrainedModel.zip");

        public static IServiceCollection AddInfrastructureRegistrations(this IServiceCollection services)
        {


            services.AddPredictionEnginePool<AggressionScoreInput, AggressionScoreOutput>()
                    .FromFile(filePath: _modelFile, watchForChanges: true);


            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IPhotoManager, PhotoManager>();
            services.AddSingleton<IAggressionScoreService, AggressionScoreService>();

            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AppMappings(services.BuildServiceProvider().GetRequiredService<IUserAccessor>()));
            }).CreateMapper());

            return services;
        }
    }
}
