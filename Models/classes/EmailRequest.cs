namespace GestionFormation.Models.classes
{
    public class EmailRequest
    {
            public string? To { get; set; }      
            public string? Subject { get; set; } 
            public string? Body { get; set; }
            public List<string>? Cc { get; set; }
    }
}
