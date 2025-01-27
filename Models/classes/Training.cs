﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("training")]
    public class Training
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("department_id")]
        public int DepartementID { get; set; }

        [Column("trainer_type_id")]
        public int TrainerTypeID { get; set; }

        [Column("theme")]
        public string? Theme { get; set; }

        [Column("objective")]
        public string? Objective { get; set; }

        [Column("place")]
        public string? Place { get; set; }

        [Column("trainer_name")]
        public string? TrainerName { get; set; }

        [Column("min_nbr")]
        public int MinNbr { get; set; }

        [Column("max_nbr")]
        public int MaxNbr { get; set; }

        [Column("creaction_date")]
        public DateTime Creation { get; set; }
    }
}
