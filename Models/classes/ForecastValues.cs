using GestionFormation.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;
using static GestionFormation.Services.CustomJsonConverters;

namespace GestionFormation.Models.classes
{
    public class ForecastValues
    {
        public string TrainingTheme { get; set; }
        public string TrainingPlace { get; set; }
        public string TrainingObjective { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }

        [JsonConverter(typeof(TimeOnlyJsonConverter))]
        public TimeOnly StartTime { get; set; }

        [JsonConverter(typeof(TimeOnlyJsonConverter))]
        public TimeOnly EndTime { get; set; }
    }
}
