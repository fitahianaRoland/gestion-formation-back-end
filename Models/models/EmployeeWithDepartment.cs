using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestion_fomation_back_end_local.Models.models
{ 
    public class EmployeeWithDepartment
    {
        public int EmployeeId { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public DateTime DateHire { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? Email { get; set; }
    }
}
