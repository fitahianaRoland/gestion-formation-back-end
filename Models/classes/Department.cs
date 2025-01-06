using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("department")]
    public class Department
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("description")]
        public string? Name { get; set; }
    }
}
