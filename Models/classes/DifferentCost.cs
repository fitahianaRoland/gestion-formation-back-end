using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    public class DifferentCost
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("description")]
        public string? Description { get; set; }
    }
}
