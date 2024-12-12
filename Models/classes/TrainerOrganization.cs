using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
	[Table("trainer_organization")]
	public class TrainerOrganization
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("trainer_organization_name")]
		public string? TrainerOrganizationName { get; set; }
	}
}
