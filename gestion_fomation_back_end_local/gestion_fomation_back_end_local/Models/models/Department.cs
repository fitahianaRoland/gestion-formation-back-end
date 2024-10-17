using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_fomation_back_end_local.Models.models
{
    [Table("department")]
    public class Department
    {
        [Key]
        [Column("Department_id")]
        public int DepartmentId { get; set; }

        [Column("Department_name")]
        public string? DepartmentName { get; set; } 
    }
}
