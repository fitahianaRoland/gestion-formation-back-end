using System;
using System.Collections.Generic;
using System.IO;

namespace GestionFormation.Services
{
    public class EmailTemplateService
    {
        public string GetEmailTemplate(Dictionary<string, string> placeholders)
        {
            string template = File.ReadAllText("Template/TrainingInvitationTemplate.txt");
            foreach (var placeholder in placeholders){
                template = template.Replace($"{{{placeholder.Key}}}", placeholder.Value);
            }
            return template;
        }

    }
}
    