namespace GestionFormation.Models.classes
{
    public class SendMailEvaluationParam
    {
        public int TrainingId { get; set; }
        public int TrainingSessionId { get; set; }
        public string TrainingTheme { get; set; }
        public List<Participants> Listparticipants { get; set; }
    }
}
