using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionFormation.Models.classes
{
    [Table("access_profile")]
    public class AccessProfile
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("profile_id")]
        public int ProfileId { get; set; }

        [Column("access_id")]
        public int AccessId { get; set; }
    }
}
