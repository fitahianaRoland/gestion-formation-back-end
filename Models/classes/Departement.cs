using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("Department")]
    public class Departement
    {
        [Key]
        [Column("Department_id")]
        public int Id { get; set; }
        [Column("Department_name")]
        public string? Nom { get; set; }
    }
}
