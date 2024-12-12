using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("training_evaluation_general_average_score")]
    public class ViewTrainingEvaluationGeneralAverageScore
    {
        [Column("training_id")]
        public int TrainingId { get; set; }

        [Column("training_session_id")]
        public int TrainingSessionId { get; set; }

        [Column("general_average_score")]
        public double GeneralAverageScore { get; set; }
    }
}
