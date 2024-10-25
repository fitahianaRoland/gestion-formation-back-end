using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestion_fomation_back_end_local.Models.models
{
    [Table("T_SAL")] 
    public class EmployeeSage
    {
        [Key]
        [Column("SA_CompteurNumero")] 
        public int EmployeeId { get; set; } 

        [Column("Telephone")] 
        public string? RegistrationNumber { get; set; } 

        [Column("Nom")] 
        public string? Name { get; set; } 

        [Column("Prenom")] 
        public string? FirstName { get; set; } 

        [Column("AdresseMelProfessionnelle")] 
        public string? Email { get; set; } 
    }
}
