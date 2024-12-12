using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("view_training_planned_status")]
    public class ViewTrainingPlannedStatus
    {
        [Column("training_id")]
        public int TrainingId { get; set; }

        [Column("trainer_type_id")]
        public int TrainerTypeId { get; set; }

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

        [Column("validation_id")]
        public int ValidationId { get; set; }

        [Column("session_status_id")]
        public int SessionStatusId { get; set; }

        [Column("session_status")]
        public string? SessionStatus { get; set; }

        [Column("session_status_value")]
        public int SessionStatusValue { get; set; }

        [NotMapped]
        public string StatusColor
        {
            get
            {
                return SessionStatusValue switch
                {
                    0 => "#f1f1e3", // Pending
                    10 => "#ffcc00", // In progress
                    _ => "#2ecc71", // Completed or other
                };
            }
        }
    }
}
