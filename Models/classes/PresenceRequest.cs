namespace GestionFormation.Models.classes
{
    public class PresenceRequest
    {
        public int TrainingId { get; set; }
        public int TrainingSessionId { get; set; }
        public int[] ListId { get; set; }
    }
}
