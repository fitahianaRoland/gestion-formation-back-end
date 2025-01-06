using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("T_SAL")]
public class EmployeSA
{
    [Key]
    [Column("SA_CompteurNumero")]
    public int Employee_id { get; set; }

    [Column("Nom")]
    public string? Name { get; set; }

    [Column("Prenom")]
    public string? FirstName { get; set; }

    [Column("AdresseMelProfessionnelle")]
    public string? Email { get; set; }

}
