using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("Internal_Training_Organisation")]
    public class InternalTraining
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("training_id")]
        public int TrainingId { get; set; }

        [Column("TrainerName")]
        public string? TrainerName { get; set; }

        [Column("validation_id")]
        public int Validation { get; set; }
    }
}
