using GestionFormation.Models.classes;
using GestionFormation.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestionFormation.Services
{
    public class GenerateJwtTokenForLink
    {
        private readonly IConfiguration _configuration;
        private readonly EvaluationTokenRepository _evaluationTokenRepository;
        public GenerateJwtTokenForLink(IConfiguration configuration,EvaluationTokenRepository evaluationTokenRepository)
        {
            _configuration = configuration;
            _evaluationTokenRepository = evaluationTokenRepository;
        }

        public async Task<string> GenerateJwtTokenForLinkFunction(int trainingId, int trainingSessionId, string trainingTheme,string email)
        {
            var linkJwt = _configuration.GetSection("LinkJwt");
            if (trainingId == null || trainingSessionId == null)
            {
                throw new ArgumentNullException("id_formation ou id_session_formation null !");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(linkJwt["Key"]);
            Console.WriteLine("123453 "+ trainingId);
            Console.WriteLine("456789 "+ trainingSessionId);
            Console.WriteLine("12378 "+ trainingTheme);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("trainingId", trainingId.ToString()),
                    new Claim("trainingSessionId", trainingSessionId.ToString()),
                    new Claim("trainingTheme", trainingTheme.ToString()),
                    new Claim("email", email.ToString()),
                }),
                //Expires = DateTime.UtcNow.AddHours(int.Parse(linkJwt["TokenExpirationHours"])),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(linkJwt["TokenExpirationMinuts"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            var evaluationToken = new EvaluationToken
            {
                Token = tokenString,
                IsUsed = false,
                ExpiryDate = DateTime.UtcNow.AddMinutes(int.Parse(linkJwt["TokenExpirationMinuts"]))
            };
            await _evaluationTokenRepository.AddAsync(evaluationToken);
            var portalLink = $"https://localhost:7008/api/TrainingEvaluation/AccessToken/{tokenString}";
            return portalLink;
        }

        public async Task<(string[], string)> ValidateLink(string token)
        {
            Console.WriteLine("********************* io iopqbgpo "+ token);
            var tokenHandler = new JwtSecurityTokenHandler();
            var linkJwt = _configuration.GetSection("LinkJwt");
            string[] stringRespone = new string[4];
            var key = Encoding.ASCII.GetBytes(linkJwt["Key"]);
            try
            {
                EvaluationToken evaluationToken = await  _evaluationTokenRepository.GetEvaluationTokenById(token);
                if (evaluationToken == null || evaluationToken.IsUsed || evaluationToken.SessionId != null)
                {
                    Console.WriteLine("gsmbgmz ato ilay erreur ");
                    throw new SecurityTokenException("Token invalide ou expiré.");
                }
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero // Pas de délai de grâce pour la validation
                }, out SecurityToken validatedToken);
                var sessionId = Guid.NewGuid().ToString();
                _evaluationTokenRepository.UpdateSessionIdNotNull(evaluationToken.Id, sessionId);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var trainingId = jwtToken.Claims.First(x => x.Type == "trainingId").Value;
                var trainingSessionId = jwtToken.Claims.First(x => x.Type == "trainingSessionId").Value;
                var trainingTheme = jwtToken.Claims.First(x => x.Type == "trainingTheme").Value;
                stringRespone[0] = trainingId;
                stringRespone[1] = trainingSessionId;
                stringRespone[2] = trainingTheme;
                stringRespone[3] = token;
                Console.WriteLine("---------------------------------------------------------------------------------------");
                foreach(var i in stringRespone)
                {
                    Console.WriteLine(i);
                }
                return (stringRespone,sessionId);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(" problem sur la validation du token :"+ ex.Message);
            }
        }
    }
}
