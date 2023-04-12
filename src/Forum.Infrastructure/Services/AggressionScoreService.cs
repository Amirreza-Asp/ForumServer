using Forum.Application.Services;
using Forum.Domain.ServicesModels;
using Microsoft.Extensions.ML;

namespace Forum.Infrastructure.Services
{
    public class AggressionScoreService : IAggressionScoreService
    {
        private readonly PredictionEnginePool<AggressionScoreInput, AggressionScoreOutput> _predictionEnginPool;

        public AggressionScoreService(PredictionEnginePool<AggressionScoreInput, AggressionScoreOutput> predictionEnginPool)
        {
            _predictionEnginPool = predictionEnginPool;
        }

        public AggressionScoreOutput Predict(String input)
        {
            return _predictionEnginPool.Predict(new AggressionScoreInput() { Comment = input });
        }
    }
}
