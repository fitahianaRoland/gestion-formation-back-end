using System.ComponentModel.DataAnnotations;

namespace GestionFormation.Models.classes
{
    public class SendScoreEvaluationParam
    {
        public int TrainingId { get; set; }
        public int TrainingSessionId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The comment cannot exceed 500 characters.")]
        public string Comment { get; set; }
        public List<TrainingEvaluationType> ListTrainingEvaluationTypeScore { get; set; }
        public string Token { get; set; }
    }
}
