using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionFormation.Models.classes
{
    [Table("UserRoleView")]
    public class UserRoleView
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("password")]
        public string Password { get; set; } = null!;

        [Column("email")]
        public string Email { get; set; } = null!;

        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("description")]
        public string RoleDescription { get; set; } = null!;
    }
}
