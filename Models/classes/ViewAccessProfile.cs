using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("view_access_profile")]
    public class ViewAccessProfile
    {
        [Column("access_id")]
        public int AccessId { get; set; }

        [Column("access_description")]
        public string AccessDescription { get; set; } = string.Empty;

        [Column("profile_id")]
        public int ProfileId { get; set; }

        [Column("profile_description")]
        public string ProfileDescription { get; set; } = string.Empty;
    }
}
