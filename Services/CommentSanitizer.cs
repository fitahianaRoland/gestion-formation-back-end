using System.Text.RegularExpressions;

namespace GestionFormation.Services
{
    public class CommentSanitizer
    {
        public string RemoveHtmlTags(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Expression régulière pour supprimer toutes les balises HTML
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
    }
}
