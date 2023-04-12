using Microsoft.ML.Data;

namespace Forum.Domain.ServicesModels
{
    public class AggressionScoreInput
    {
        [LoadColumn(1)]
        public String Comment { get; set; }

        [LoadColumn(0), ColumnName("Label")]
        public bool IsAggressive { get; set; }
    }
}
