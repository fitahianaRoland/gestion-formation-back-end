using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("training_session")]
    public class Session
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("training_id")]
        public int TrainingId { get; set; }

        [ForeignKey("TrainingId")]
        public Training Training { get; set; } 

        [Column("start_date")]
        public DateTime? StartDate { get; set; }

        [Column("end_date")]
        public DateTime? EndDate { get; set; }

        [Column("morning_start_hour")]
        public TimeSpan? MorningStartHour { get; set; }

        [Column("morning_end_hour")]
        public TimeSpan? MorningEndHour { get; set; }

        [Column("afternoon_start_hour")]
        public TimeSpan? AfternoonStartHour { get; set; }

        [Column("afternoon_end_hour")]
        public TimeSpan? AfternoonEndHour { get; set; }

        [Column("reason_refusal")]
        public string? ReasonRefusal { get; set; }

        [Column("validation_id")]
        public int ValidationId { get; set; }

        [ForeignKey("ValidationId")]
        public State Validation { get; set; } 
    }
}
