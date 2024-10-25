using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("training_organization")]
    public class TrainingOrganization
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("training_organization")]
        public string? Nom { get; set; }
    }
}
