using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("view_role_profile")]
    public class ViewRoleProfile
    {
        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("role_description")]
        public string RoleDescription { get; set; }

        [Column("profile_id")]
        public int ProfileId { get; set; }

        [Column("profile_description")]
        public string ProfileDescription { get; set; }
    }
}
