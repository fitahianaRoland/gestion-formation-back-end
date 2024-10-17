using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_fomation_back_end_local.Models.models
{
    [Table("training_session")]
    public class TrainingSession
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("training_id")]
        public int TrainingId { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("End_date")]
        public DateTime EndDate { get; set; }

        [Column("start_hour")]
        public TimeSpan StartHour { get; set; }

        [Column("end_hour")]
        public TimeSpan EndHour { get; set; }

    }
}
