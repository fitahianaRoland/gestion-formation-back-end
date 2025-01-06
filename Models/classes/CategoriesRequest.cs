using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("categories_request_offer")]
    public class CategoriesRequest
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("description")]
        public string? Description { get; set; }
    }
}
