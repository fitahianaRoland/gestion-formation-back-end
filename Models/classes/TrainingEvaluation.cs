using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("training_evaluation")]
    public class TrainingEvaluation
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("training_id")]
        public int TrainingId { get; set; }

        [Column("training_session_id")]
        public int TrainingSessionId { get; set; }

        [Column("comment")]
        public string Comment { get; set; }
        
        public TrainingEvaluation() { }
        public TrainingEvaluation(int trainingId,int trainingSessionId, string comment) { 
            TrainingId = trainingId;
            TrainingSessionId = trainingSessionId;
            Comment = comment;
        }

    }
}
