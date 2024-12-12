using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.Classes
{
    [Table("training_session_status")]
    public class TrainingSessionStatus
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("value")]
        public int Value { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}
