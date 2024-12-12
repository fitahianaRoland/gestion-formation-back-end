using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GestionFormation.Models.repository
{
    public class ForecastPresenceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly TrainingRepository _trainingRepository;

        public ForecastPresenceRepository(ApplicationDbContext context, TrainingRepository trainingRepository)
        {
            _context = context;
            _trainingRepository = trainingRepository;
        }

        public async Task AddForecast(EmailRequest emailRequest)
        {
            List<ForecastPresence> listForcast = new List<ForecastPresence>();
            List<Participants> listParticipants = JsonSerializer.Deserialize<List<Participants>>(emailRequest.ListOfEmployee);
            if(!listParticipants.Any()) {  throw new ArgumentException("ajouter des participants !");  }
            foreach (var forecast in listParticipants)
            {
                Console.WriteLine(" ************" + forecast.Name + " - " + forecast.FirstName + " - " + forecast.Email);
            }

            //condition de validation des nombres de participants :
            Training training = await _trainingRepository.FindById(emailRequest.TrainingId);
            int participantsNumber = listParticipants.Count();
            Console.WriteLine(training.MinNbr + " < *** " + participantsNumber + " ***  > " + training.MaxNbr);
            if (participantsNumber < training.MinNbr || participantsNumber > training.MaxNbr)
            {
                throw new ArgumentException("les nombres de participants doit etre compris entre " + training.MinNbr + " et " + training.MaxNbr);
            }

            int training_id = emailRequest.TrainingId;
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
                    TrainingId = training_id,
                    TrainingSessionId = training_session_id,
                    Name = participant.Name,
                    FirstName = participant.FirstName,
                    Email = participant.Email,
                    State = absent_state
                });
            }
            foreach (var forecast in listForcast)
            {
                Console.WriteLine(" .................... " + forecast.Name + " " + forecast.FirstName + " " + forecast.TrainingSessionId + " " + forecast.State);
            }
            await _context.forecastPresence.AddRangeAsync(listForcast);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ForecastPresence>> ListForecast(int trainingId, int trainingSessionId, string name, int state)
        {
            if(name == null)
            {
                Console.WriteLine("Tsy Misy valeur le searchname");
                return await _context.forecastPresence.Where(e => e.TrainingId == trainingId && e.TrainingSessionId == trainingSessionId && e.State == state).ToListAsync();
            }
            else
            {
                Console.WriteLine("Misy valeur le searchname");
                return await _context.forecastPresence.Where(e => e.TrainingId == trainingId && e.TrainingSessionId == trainingSessionId && e.State == state && (e.Name.Contains(name) || e.FirstName.Contains(name)) ).ToListAsync();
            }
        }

        public async Task Presence(int trainingId, int trainingSessionId, int[] listId)
        {
            try
            {
                string employeeIds = listIntIntoString(listId);
                string sql = $@"
                    UPDATE forecast_presence
                    SET state = 1
                    WHERE training_id = {trainingId}
                      AND training_session_id = {trainingSessionId}
                      AND id IN ({employeeIds})";
                await _context.Database.ExecuteSqlRawAsync(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("error update!!");
            }
        }

        public string listIntIntoString(int[] listint)
        {
            if (listint == null || listint.Length == 0)
            {
                return string.Empty;
            }
            return string.Join(",", listint);
        }
    }
}
