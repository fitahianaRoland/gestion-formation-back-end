using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GestionFormation.Models.classes
{
    [Table("forecast_presence")]
    public class ForecastPresence
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("training_id")]
        public int TrainingId { get; set; }

        [Column("training_session_id")]
        public int TrainingSessionId { get; set; }

        [Column("name")]
        public string Name { get; set; } 

        [Column("FirstName")]
        public string? FirstName { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("state")]
        public int? State { get; set; }
    }

}
