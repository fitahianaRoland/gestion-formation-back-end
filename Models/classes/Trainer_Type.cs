using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionFormation.Models.classes
{
    [Table("trainer_type")]
    public class Trainer_Type
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("description")]
        public string? Description { get; set; }
    }
}
