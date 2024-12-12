using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("view_training_session_completed_status")]
    public class ViewTrainingSessionCompletedStatus
    {
        [Column("session_id")]
        public int SessionId { get; set; }

        [Column("training_id")]
        public int TrainingId { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("start_hour")]
        public TimeSpan StartHour { get; set; }

        [Column("end_hour")]
        public TimeSpan EndHour { get; set; }

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
                    20 => "#ffcc00", // In progress
                    _ => "#2ecc71", // Completed or other
                };
            }
        }
    }
}
