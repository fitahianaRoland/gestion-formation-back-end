using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("app_user")]
    public class AppUser
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Nom { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("Email")]
        public string? Email { get; set; }

    }
}
