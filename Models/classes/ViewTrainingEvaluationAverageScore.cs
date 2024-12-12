using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("view_training_evaluation_average_score")]
    public class ViewTrainingEvaluationAverageScore
    {
        [Column("training_id")]
        public int TrainingId { get; set; }

        [Column("training_session_id")]
        public int TrainingSessionId { get; set; }

        [Column("training_evaluation_type_id")]
        public int TrainingEvaluationTypeId { get; set; }

        [Column("training_evaluation_type_name")]
        public string TrainingEvaluationTypeName { get; set; } = null!;

        [Column("average_score")]
        public double AverageScore { get; set; }

        [NotMapped]
        public double GetAverageScorePercentage
        {
            get{ 
                return (AverageScore * 100) / 5;
            }
        }
    }
}
