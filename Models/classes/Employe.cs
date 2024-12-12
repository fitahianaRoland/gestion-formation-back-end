using GestionFormation.Models.classes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Employee")]
public class Employee
{
    [Key]
    [Column("Employee_id")]
    public int Employee_id { get; set; }

    [Required]
    [Column("Registration_number")]
    public string? Registration_number { get; set; }

    [Required]
    [Column("Name")]
    public string? Name { get; set; }

    [Required]
    [Column("FirstName")]
    public string? FirstName { get; set; }

    [Required]
    [Column("Date_hire")]
    public DateTime Date_hire { get; set; }

    [Column("department_id")]
    public int department_id { get; set; }


    [Column("Email")]
    public string? Email { get; set; }

}
