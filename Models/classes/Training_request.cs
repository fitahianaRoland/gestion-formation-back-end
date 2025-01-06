
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("training_request")]
    public class Training_request
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("training_id")]
        public int TrainingID { get; set; }

        [Column("Name")]
        public string? Name { get; set; }

        [Column("FirstName")]
        public string? FirstName { get; set; }

        [Column("Email")]
        public string? Email { get; set; }

        [Column("request_date")]
        public DateTime? RequestDate { get; set; }
    }
}
