using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("view_training_evaluation_response_count")]
    public class ViewTrainingEvaluationResponseCount
    {
        [Key]
        [Column("training_id")]
        public int TrainingId { get; set; }

        [Column("training_session_id")]
        public int TrainingSessionId { get; set; }

        [Column("number_of_responses")]
        public int NumberOfResponses { get; set; }
    }
}
