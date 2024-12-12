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

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }
    }
}
