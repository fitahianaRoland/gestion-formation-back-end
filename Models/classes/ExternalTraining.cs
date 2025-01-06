using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
	[Table("External_Training_Organisation")]
	public class ExternalTraining
	{
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("training_id")]
        public int TrainingId { get; set; }

        [Column("trainer_organization_id")]
        public int TrainerOrganizationId { get; set; }

        [Column("categories_request_offer_id")]
        public int CategoriesId { get; set; } 

        [Column("name")]
        public string? Name { get; set; }

        [Column("FirstName")]
        public string? FirstName { get; set; }

        [Column("Phone_number")]
        public string? PhoneNumber { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("validation_id")]
        public int ValidationId { get; set; }
    }
}
