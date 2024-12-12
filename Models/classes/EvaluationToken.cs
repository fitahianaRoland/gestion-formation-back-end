using GestionFormation.Models.classes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("evaluation_token")]
    public class EvaluationToken
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("token")]
        [Required]
        public string Token { get; set; } = null!;

        [Column("is_used")]
        public bool IsUsed { get; set; }

        [Column("expiry_date")]
        public DateTime ExpiryDate { get; set; }

        [Column("session_id")]
        public string? SessionId {  get; set; }

        //[Column("training_id")]
        //public int TrainingId { get; set; }
         
        //[Column("training_session_id")]
        //public int TrainingSessionId { get; set; }    

    }
}
