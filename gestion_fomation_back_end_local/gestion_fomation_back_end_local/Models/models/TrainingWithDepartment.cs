using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_fomation_back_end_local.Models.models
{

    public class TrainingWithDepartment
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int TrainerTypeId { get; set; }
        public string? Theme { get; set; }
        public string? Objective { get; set; }
        public string? Place { get; set; }
        public string? TrainerName { get; set; }
        public int MinNbr { get; set; }
        public int MaxNbr { get; set; }
        public DateTime CreationDate { get; set;}
    }
}
