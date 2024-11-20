using GestionFormation.Models.classes;
using System.Text.Json;

namespace GestionFormation.Models.repository
{
    public class ForecastPresenceRepository
    {
        private readonly ApplicationDbContext _context;

        public ForecastPresenceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddForecast(EmailRequest emailRequest)
        {
            List<ForecastPresence> listForcast = new List<ForecastPresence>();
            List<Participants> listParticipants = JsonSerializer.Deserialize<List<Participants>>(emailRequest.ListOfEmployee);
            foreach (var forecast in listParticipants)
            {
                Console.WriteLine(" ************" + forecast.Name + " - " + forecast.FirstName + " - " + forecast.Email);
            }
            int training_session_id = emailRequest.TrainingSessionId;
            int absent_state = 0;
            if (listParticipants == null || !listParticipants.Any())
            {
                Console.WriteLine(" ************ ts nety ");
                throw new ArgumentException("La liste des participants est vide");
            }
            foreach (var participant in listParticipants)
            {
                listForcast.Add(new ForecastPresence
                {
                    TrainingSessionId = training_session_id,  
                    Name = participant.Name,         
                    FirstName = participant.FirstName, 
                    Email = participant.Email,        
                    State = absent_state
                });
            }
            foreach(var forecast in listForcast)
            {
                Console.WriteLine(" .................... " + forecast.Name + " " + forecast.FirstName + " " + forecast.TrainingSessionId + " " + forecast.State);
            }
            await _context.forecastPresence.AddRangeAsync(listForcast);
            await _context.SaveChangesAsync();
        }

    }
}
