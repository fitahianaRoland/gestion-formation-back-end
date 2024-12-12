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
		public int TrainerorganizationId { get; set; }

		[Column("TrainerName")]
		public string? TrainerName { get; set; }
	}
}
