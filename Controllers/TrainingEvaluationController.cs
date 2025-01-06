using GestionFormation.Models.classes;
using GestionFormation.Repository;
using GestionFormation.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionFormation.Interfaces;

using Microsoft.EntityFrameworkCore;
using GestionFormation.Models.Repository;
using static GestionFormation.Controllers.TrainingEvaluationController;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace GestionFormation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingEvaluationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public readonly ItemTrainingEvaluationRepository _itemTrainingEvaluationRepository;
        public readonly TrainingEvaluationRepository _TrainingEvaluationRepository;
        public readonly TrainingEvaluationScoreRepository _TrainingEvaluationScoreRepository;
        private readonly IEmailService _emailService;
        private readonly TrainingEvaluationStatusRepository _trainingEvaluationStatusRepository;
        private readonly SendingStatusRepository _sendingStatusRepository;
        private readonly GenerateJwtTokenForLink _generateJwtTokenForLink;
        private readonly int _waiting_for_result_status;
        private readonly int _validated_result_status;
        private readonly CommentSanitizer _commentSanitizer;
        private readonly EvaluationTokenRepository _evaluationTokenRepository;
        private readonly IConfiguration _configuration;

        public TrainingEvaluationController(
            ApplicationDbContext context,
            ItemTrainingEvaluationRepository itemTrainingEvaluationRepository,
            TrainingEvaluationRepository trainingEvaluationRepository,
            TrainingEvaluationScoreRepository trainingEvaluationScoreRepository,
            IEmailService emailService,
            TrainingEvaluationStatusRepository trainingEvaluationStatusRepository,
            SendingStatusRepository sendingStatusRepository,
            GenerateJwtTokenForLink generateJwtTokenForLink,
            CommentSanitizer commentSanitizer,
            EvaluationTokenRepository evaluationTokenRepository,
            IConfiguration configuration
            )
        {
            _context = context;
            _itemTrainingEvaluationRepository = itemTrainingEvaluationRepository;
            _TrainingEvaluationRepository = trainingEvaluationRepository;
            _TrainingEvaluationScoreRepository = trainingEvaluationScoreRepository;
            _emailService = emailService;
            _trainingEvaluationStatusRepository = trainingEvaluationStatusRepository;
            _sendingStatusRepository = sendingStatusRepository;
            _generateJwtTokenForLink = generateJwtTokenForLink;
            _commentSanitizer = commentSanitizer;
            _evaluationTokenRepository = evaluationTokenRepository;
            _configuration = configuration;
            _waiting_for_result_status = 10;
            _validated_result_status = 20;
        }

        [HttpGet("GetItemTrainingEvaluation/{trainingId}/{trainingSessionId}")]
        public async Task<IActionResult> GetItemTrainingEvaluation(int trainingId, int trainingSessionId)
        {
            try
            {
                var listItemTrainingSessionEvaluation = await _itemTrainingEvaluationRepository.GetByTrainingIdTrainingSessionIdAsync(trainingId, trainingSessionId);
                return Ok(listItemTrainingSessionEvaluation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("AccessToken/{token}")]
        public async Task<IActionResult> AccessToken(string token)
        {
            Console.WriteLine("coucou accexx toke 1____________________________________________________________________");
            var linkJwt = _configuration.GetSection("LinkJwt");
            try
            {
                var (stringRespone, sessionId) = await _generateJwtTokenForLink.ValidateLink(token);
                Console.WriteLine("coucou accexx toke 2____________________________________________________________________");
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddMinutes(int.Parse(linkJwt["TokenExpirationMinuts"]))
                };
                Console.WriteLine("àààààààààààààààààààààààààààààààààààààààààààààààààààààé");
                foreach (var i in stringRespone)
                {
                    Console.WriteLine(i);
                }
                Response.Cookies.Append("session_id", sessionId, cookieOptions);
                Response.Cookies.Append("trainingId", stringRespone[0], cookieOptions);
                Response.Cookies.Append("trainingSessionId", stringRespone[1], cookieOptions);
                Response.Cookies.Append("trainingTheme", stringRespone[2], cookieOptions);
                Response.Cookies.Append("token", stringRespone[3], cookieOptions);
                return Redirect($"http://localhost:3000/evaluation/EvaluationFormPage");
            }
            catch (Exception ex)
            {
                return Unauthorized($"Token validation failed: {ex.Message}");
            }
        }

        [EnableCors("AllowFrontend")]
        [HttpGet("GetCookies")]
        public async Task<IActionResult> GetCookies()
        {
            Console.WriteLine(" coucoucoucocuopcuopcuopcucopucopcupocuocpucpocup coucocopcoucoucoucuccoucoucoucoucoucoucoucoucou");
            var token = Request.Cookies["token"];
            EvaluationToken evaluationToken = await _evaluationTokenRepository.GetEvaluationTokenById(token);
            Console.WriteLine(evaluationToken.IsUsed + " !! "+evaluationToken.Token );
            if (evaluationToken == null || evaluationToken.IsUsed )
            {
                throw new SecurityTokenException("Erreur: Token invalide ou expiré.");
            }
            var sessionId = Request.Cookies["session_id"];
            var trainingId = Request.Cookies["trainingId"];
            var trainingSessionId = Request.Cookies["trainingSessionId"];
            var trainingTheme = Request.Cookies["trainingTheme"];

            Console.WriteLine(" id session " + sessionId);

            if (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(trainingId) ||
                string.IsNullOrEmpty(trainingSessionId) || string.IsNullOrEmpty(trainingTheme) || string.IsNullOrEmpty(token))
            {
                return Unauthorized("L'un des cookies est manquant ou invalide.");
            }
            Console.WriteLine("get cookie :"+trainingTheme);
            var cookiesData = new
            {
                SessionId = sessionId,
                TrainingId = trainingId,
                TrainingSessionId = trainingSessionId,
                TrainingTheme = trainingTheme,
                Token = token 
            };
            Console.WriteLine("token :"+cookiesData.Token);
            return Ok(cookiesData);
        }

        [HttpPost("SaveOnExit")]
        public async Task<IActionResult> SaveOnExit([FromBody] string token)
        {
            try
            {
                Console.WriteLine($"{token}" + "ppppppppppppppppppppppppppptjozpnopqn");
                EvaluationToken evaluationToken = await _evaluationTokenRepository.GetEvaluationTokenById(token);
                _evaluationTokenRepository.UpdateSessionIdNull(evaluationToken.Id);
                return Ok("success !");
            }
            catch(Exception ex) {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        [HttpPost("AddTrainingEvaluationScore")]
        public async Task<IActionResult> AddTrainingEvaluationScore([FromBody] SendScoreEvaluationParam sendScoreEvaluationParam)
        {

            int trainingId = sendScoreEvaluationParam.TrainingId;
            int trainingSessionId = sendScoreEvaluationParam.TrainingSessionId;
            string comment = sendScoreEvaluationParam.Comment;
            string token = sendScoreEvaluationParam.Token;
            List<TrainingEvaluationType> trainingEvaluationTypes = sendScoreEvaluationParam.ListTrainingEvaluationTypeScore;
            try
            {
                Console.WriteLine($"Training ID: {sendScoreEvaluationParam.TrainingId}");
                Console.WriteLine($"Training Session ID: {sendScoreEvaluationParam.TrainingSessionId}");
                Console.WriteLine($"Comment: {sendScoreEvaluationParam.Comment}");
                Console.WriteLine($"token: {sendScoreEvaluationParam.Token}");
                Console.WriteLine("Training Evaluation Types and Scores:");
                foreach (var evaluation in sendScoreEvaluationParam.ListTrainingEvaluationTypeScore)
                {
                    Console.WriteLine($"  - ID: {evaluation.Id}, Name: {evaluation.Name}, Score: {evaluation.Rating}");
                }
                if (string.IsNullOrWhiteSpace(sendScoreEvaluationParam.Comment) || sendScoreEvaluationParam.Comment.Length > 500)
                {
                    return BadRequest("Le commentaire est invalide.");
                }
                //var sanitizedContent = System.Net.WebUtility.HtmlEncode(sendScoreEvaluationParam.Comment); encodage 
                comment  = _commentSanitizer.RemoveHtmlTags(comment);
                TrainingEvaluation trainingEvaluation = new TrainingEvaluation(trainingId, trainingSessionId, comment);
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        int sending_status_id = await _sendingStatusRepository.GetIdByValueAsync(_validated_result_status);
                        TrainingEvaluationStatus trainingEvaluationStatus = new TrainingEvaluationStatus(trainingId, trainingSessionId, sending_status_id);
                        await _trainingEvaluationStatusRepository.AddAsync(trainingEvaluationStatus);
                        _context.trainingEvaluation.Add(trainingEvaluation);
                        await _context.SaveChangesAsync();
                        var scores = TrainingEvaluationScore.Fusion_TrainingEvaluationId_TrainingEvaluationTypeId(trainingEvaluation.Id, sendScoreEvaluationParam.ListTrainingEvaluationTypeScore);
                        _context.trainingEvaluationScore.AddRange(scores);
                        await _context.SaveChangesAsync();
                        EvaluationToken evaluationToken = await _evaluationTokenRepository.GetEvaluationTokenById(token);
                        _evaluationTokenRepository.UpdateIsUsed(evaluationToken.Id, true);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception("Une erreur s'est produite lors de l'ajout de l'évaluation et des scores."+ ex.Message);
                    }
                }


                return Ok("insertion score type evaluation par formation par session réussi!");
            }
            catch (Exception ex)
            {
                return BadRequest("hello" + ex.Message);
            }
        }

        [HttpPost("SendLinkPortal")]
        public async Task<IActionResult> SendLinkPortal([FromBody] SendMailEvaluationParam sendMailEvaluationParam)
        {
            try
            {
                Console.WriteLine("hello send link");
                int trainingId = sendMailEvaluationParam.TrainingId;
                int trainingSessionId = sendMailEvaluationParam.TrainingSessionId;
                Console.WriteLine(trainingId);
                Console.WriteLine(trainingSessionId);
                string trainingTheme = sendMailEvaluationParam.TrainingTheme;
                List<Participants> participants = sendMailEvaluationParam.Listparticipants;
                if (!participants.Any()) { throw new ArgumentException("ajouter des participants !"); }
                try
                {
                    await _emailService.SendLinkEmailParticipatsAsync(trainingId,trainingSessionId, trainingTheme,participants);
                    int sending_status_id = await _sendingStatusRepository.GetIdByValueAsync(_waiting_for_result_status);
                    Console.WriteLine("************** id type etat envoie evaluation :"+sending_status_id);
                        TrainingEvaluationStatus trainingEvaluationStatus = new TrainingEvaluationStatus(trainingId,trainingSessionId,sending_status_id);
                    await _trainingEvaluationStatusRepository.AddAsync(trainingEvaluationStatus);
                    //await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Une erreur s'est produite lors de l'envoie du lien d'évaluation .", ex);
                }
                return Ok(" lien envoyer ! ");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }

        [HttpGet("GetTrainingEvaluationData/{trainingId}/{trainingSessionId}")]
        public async Task<IActionResult> GetTrainingEvaluationData(int trainingId, int trainingSessionId)
        {
            try
            {
                var listTrainingEvaluationAverageScore = await _TrainingEvaluationRepository.GetAverageScore(trainingId, trainingSessionId);
                var trainingEvaluationGetGeneralAverageScore = await _TrainingEvaluationRepository.GetGeneralAverageScore(trainingId, trainingSessionId);
                var trainingEvaluationResponseCount = await _TrainingEvaluationRepository.GetTrainingEvaluationResponseCount(trainingId, trainingSessionId);
                var comments = await _TrainingEvaluationRepository.GetComment(trainingId, trainingSessionId);
                return Ok(new
                {
                    ListTrainingEvaluationAverageScore= listTrainingEvaluationAverageScore,
                    TrainingEvaluationGetGeneralAverageScore= trainingEvaluationGetGeneralAverageScore,
                    TrainingEvaluationResponseCount= trainingEvaluationResponseCount,
                    Comments= comments
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public class TokenRequest
        {
            public string Token { get; set; }
        }
    }
}
