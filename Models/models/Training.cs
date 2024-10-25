using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_fomation_back_end_local.Models.models
{

    [Table("training")]
    public class Training
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("department_id")]
        public int DepartmentId { get; set; }

        [Column("trainer_type_id")]
        public int TrainerTypeId { get; set; }

        [Column("theme")]
        public string? Theme { get; set; }

        [Column("objective")]
        public string? Objective { get; set; }

        [Column("place")]
        public string? Place { get; set; }

        [Column("trainer_name")]
        public string? TrainerName{ get; set; }

        [Column("min_nbr")]
        public int MinNbr { get; set; }

        [Column("max_nbr")]
        public int MaxNbr { get; set; }

        [Column("creation_date")]
        public DateTime CreationDate { get; set; }

    }
}
