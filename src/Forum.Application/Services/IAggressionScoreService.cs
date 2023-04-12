using Forum.Domain.ServicesModels;

namespace Forum.Application.Services
{
    public interface IAggressionScoreService
    {
        public AggressionScoreOutput Predict(String input);
    }
}
