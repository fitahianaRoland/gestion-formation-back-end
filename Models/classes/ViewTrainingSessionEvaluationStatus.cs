using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("view_training_session_evaluation_status")]
    public class ViewTrainingSessionEvaluationStatus
    {
        [Column("training_id")]
        public int TrainingId { get; set; }

        [Column("training_validation_status")]
        public int TrainingValidationStatus { get; set; }

        [Column("training_session_id")]
        public int TrainingSessionId { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("start_hour")]
        public TimeSpan StartHour { get; set; }

        [Column("end_hour")]
        public TimeSpan EndHour { get; set; }

        [Column("training_session_validation_status")]
        public int TrainingSessionValidationStatus { get; set; }

        [Column("training_session_validation_value")]
        public int TrainingSessionValidationValue { get; set; } 

        [Column("sending_status_value")]
        public int SendingStatusValue { get; set; }

        [NotMapped]
        public string Color
        {
            get
            {
                return SendingStatusValue switch
                {
                    0 => "#f1f1e3",
                    10 => "#5dade2",
                    _ => "#2ecc71",
                };
            }
        }
    }
}
