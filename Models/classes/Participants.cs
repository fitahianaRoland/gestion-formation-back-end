using System.Text.Json.Serialization;

namespace GestionFormation.Models.classes
{
    public class Participants
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

    }
}
