using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("prospecting_detail")]
    public class ProspectingDetail
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("external_training")]
        public int ExternalTrainingId { get; set; }

        [Column("different_cost_id")]
        public int DifferentCostId { get; set; } // Coût forfaitaire, coût participants, coût administratif

        [Column("description")]
        public string? Description { get; set; }

        [Column("unit_price")]
        public decimal UnitPrice { get; set; }
    }
}
