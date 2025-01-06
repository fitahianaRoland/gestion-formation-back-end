using GestionFormation.Models.classes;
using System;
using System.Collections.Generic;

namespace GestionFormation.Services
{
    public class TrainingService
    {
        // Méthode pour séparer les sessions planifiées par jour et heure
        public IEnumerable<object> TrainingSessionPlannedSeparerSessionsParJourEtHeure(IEnumerable<ViewTrainingSessionPlannedForCalendar> sessions, string typeSession)
        {
            var resultats = new List<object>();

            foreach (var session in sessions)
            {
                var dateDebut = session.StartDate.Date;
                var dateFin = session.EndDate.Date;

                for (var dateCourante = dateDebut; dateCourante <= dateFin; dateCourante = dateCourante.AddDays(1))
                {
                    // Ajouter une session pour le matin si disponible
                    if (session.MorningStartHour.HasValue && session.MorningEndHour.HasValue)
                    {
                        var startMorning = session.MorningStartHour.Value.ToString(@"hh\:mm");
                        var endMorning = session.MorningEndHour.Value.ToString(@"hh\:mm");

                        resultats.Add(new
                        {
                            SessionId = session.SessionId,
                            TrainingId = session.TrainingId,
                            Title = $"{session.Theme} - Matin - {typeSession}",
                            Date = dateCourante.ToString("yyyy-MM-dd"),
                            Start = $"{dateCourante:yyyy-MM-dd}T{startMorning}",
                            End = $"{dateCourante:yyyy-MM-dd}T{endMorning}",
                            Status = session.SessionStatus,
                            ColorStatus = session.StatusColor
                        });
                    }

                    // Ajouter une session pour l'après-midi si disponible
                    if (session.AfternoonStartHour.HasValue && session.AfternoonEndHour.HasValue)
                    {
                        var startAfternoon = session.AfternoonStartHour.Value.ToString(@"hh\:mm");
                        var endAfternoon = session.AfternoonEndHour.Value.ToString(@"hh\:mm");

                        resultats.Add(new
                        {
                            SessionId = session.SessionId,
                            TrainingId = session.TrainingId,
                            Title = $"{session.Theme} - Après-midi - {typeSession}",
                            Date = dateCourante.ToString("yyyy-MM-dd"),
                            Start = $"{dateCourante:yyyy-MM-dd}T{startAfternoon}",
                            End = $"{dateCourante:yyyy-MM-dd}T{endAfternoon}",
                            Status = session.SessionStatus,
                            ColorStatus = session.StatusColor
                        });
                    }
                }
            }
            return resultats;
        }


        // Méthode pour séparer les sessions réalisées par jour et heure
        public IEnumerable<object> TrainingSessionCompletedSeparerSessionsParJourEtHeure(IEnumerable<ViewTrainingSessionCompletedForCalendar> sessions, string typeSession)
        {
            var resultats = new List<object>();

            foreach (var session in sessions)
            {
                var dateDebut = session.StartDate.Date;
                var dateFin = session.EndDate.Date;

                for (var dateCourante = dateDebut; dateCourante <= dateFin; dateCourante = dateCourante.AddDays(1))
                {
                    // Ajouter une session pour le matin si disponible
                    if (session.MorningStartHour.HasValue && session.MorningEndHour.HasValue)
                    {
                        var startMorning = session.MorningStartHour.Value.ToString(@"hh\:mm");
                        var endMorning = session.MorningEndHour.Value.ToString(@"hh\:mm");

                        resultats.Add(new
                        {
                            SessionId = session.SessionId,
                            TrainingId = session.TrainingId,
                            Title = $"{session.Theme} - Matin - {typeSession}",
                            Date = dateCourante.ToString("yyyy-MM-dd"),
                            Start = $"{dateCourante:yyyy-MM-dd}T{startMorning}",
                            End = $"{dateCourante:yyyy-MM-dd}T{endMorning}",
                            Status = session.SessionStatus,
                            ColorStatus = session.StatusColor
                        });
                    }

                    // Ajouter une session pour l'après-midi si disponible
                    if (session.AfternoonStartHour.HasValue && session.AfternoonEndHour.HasValue)
                    {
                        var startAfternoon = session.AfternoonStartHour.Value.ToString(@"hh\:mm");
                        var endAfternoon = session.AfternoonEndHour.Value.ToString(@"hh\:mm");

                        resultats.Add(new
                        {
                            SessionId = session.SessionId,
                            TrainingId = session.TrainingId,
                            Title = $"{session.Theme} - Après-midi - {typeSession}",
                            Date = dateCourante.ToString("yyyy-MM-dd"),
                            Start = $"{dateCourante:yyyy-MM-dd}T{startAfternoon}",
                            End = $"{dateCourante:yyyy-MM-dd}T{endAfternoon}",
                            Status = session.SessionStatus,
                            ColorStatus = session.StatusColor
                        });
                    }
                }
            }
            return resultats;
        }
    }
}
