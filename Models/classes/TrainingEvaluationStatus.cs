using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("training_evaluation_status")]
    public class TrainingEvaluationStatus
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("training_id")]
        public int TrainingId { get; set; }

        //[ForeignKey("TrainingId")]
        //public virtual Training Training { get; set; } // Relation avec la table 'training'

        [Column("training_session_id")]
        public int TrainingSessionId { get; set; }

        //[ForeignKey("TrainingSessionId")]
        //public virtual TrainingSession TrainingSession { get; set; } // Relation avec la table 'training_session'

        [Column("sending_status_id")]
        public int SendingStatusId { get; set; }

        //[ForeignKey("SendingStatusId")]
        //public virtual SendingStatus SendingStatus { get; set; } // Relation avec la table 'sending_status'

        public TrainingEvaluationStatus() { }

        public TrainingEvaluationStatus (int trainingId,int trainingSessionid, int status) { 
            TrainingId = trainingId;
            TrainingSessionId = trainingSessionid;
            SendingStatusId = status;
        }
    }
}
