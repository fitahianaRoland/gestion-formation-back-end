using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("view_training_session_planned_for_calendar")]
    public class ViewTrainingSessionPlannedForCalendar
    {
        [Column("session_id")]
        public int SessionId { get; set; }

        [Column("training_id")]
        public int TrainingId { get; set; }

        [Column("theme")]
        public string Theme { get; set;  }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("morning_start_hour")]
        public TimeSpan? MorningStartHour { get; set; }

        [Column("morning_end_hour")]
        public TimeSpan? MorningEndHour { get; set; }

        [Column("afternoon_start_hour")]
        public TimeSpan? AfternoonStartHour { get; set; }

        [Column("afternoon_end_hour")]
        public TimeSpan? AfternoonEndHour { get; set; }

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
                return "#ffcc00";
            }
        }
    }
}
