using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("sending_status")]
    public class SendingStatus
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("value")]
        public int Value { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}
