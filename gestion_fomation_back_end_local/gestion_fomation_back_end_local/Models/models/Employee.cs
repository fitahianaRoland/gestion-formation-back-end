using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestion_fomation_back_end_local.Models.models
{
    [Table("Employee")] 
    public class Employee
    {
        [Key]
        [Column("employee_id")] 
        public int EmployeeId { get; set; } 

        [Column("registration_number")] 
        public string? RegistrationNumber { get; set; } 

        [Column("name")] 
        public string? Name { get; set; } 

        [Column("firstName")] 
        public string? FirstName { get; set; } 

        [Column("date_hire")] 
        public DateTime DateHire { get; set; } 

        [Column("department_id")] 
        public int DepartmentId { get; set; } 

        [Column("email")] 
        public string? Email { get; set; } 
    }
}
