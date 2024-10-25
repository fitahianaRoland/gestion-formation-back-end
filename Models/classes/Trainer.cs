using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("trainer")]
    public class Trainer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("trainer")]
        public string? Nom { get; set; }
    }
}
