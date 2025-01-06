using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionFormation.Models.classes
{
    [Table("state")]
    public class State
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("value")]
        public int? Value { get; set; }

        [Column("description")]
        public string? Description { get; set; }
    }
}
