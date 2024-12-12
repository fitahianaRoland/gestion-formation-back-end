using Microsoft.AspNetCore.Mvc;

namespace GestionFormation.Models.classes
{
    public class EmailRequest
    {
        public int TrainingId { get; set;  }
        public int TrainingSessionId { get; set; }
        public string ListOfEmployee { get; set; }
        public string FormValues { get; set; }
        public IFormFile? File { get; set; }
        public bool SendEmail { get; set; }
    }
}
