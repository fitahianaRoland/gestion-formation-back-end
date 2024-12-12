using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionFormation.Models.classes
{
    [Table("view_training_evaluation_status")]
    public class ViewTrainingEvaluationStatus
    {
        [Column("training_id")]
        public int TrainingId { get; set; }

        [Column("theme")]
        public string? Theme { get; set; }

        [Column("objective")]
        public string? Objective { get; set; }

        [Column("place")]
        public string? Place { get; set; }

        [Column("trainer_name")]
        public string? TrainerName { get; set; }

        [Column("min_nbr")]
        public int MinNbr { get; set; }

        [Column("max_nbr")]
        public int MaxNbr { get; set; }

        [Column("creation_date")]
        public DateTime CreationDate { get; set; }

        [Column("training_validation_id")]
        public int TrainingValidationId { get; set; }

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
