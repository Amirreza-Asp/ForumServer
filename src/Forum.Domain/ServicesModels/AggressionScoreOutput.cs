using Microsoft.ML.Data;

namespace Forum.Domain.ServicesModels
{
    public class AggressionScoreOutput
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }
        public float Probability { get; set; }
    }
}
