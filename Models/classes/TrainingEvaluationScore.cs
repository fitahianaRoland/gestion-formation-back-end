using GestionFormation.Models.classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Models.classes
{
    [Table("training_evaluation_score")]
    public class TrainingEvaluationScore
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("training_evaluation_id")]
        public int TrainingEvaluationId { get; set; }

        //[ForeignKey("TrainingEvaluationId")]
        //public TrainingEvaluation TrainingEvaluation { get; set; }

        [Column("training_evaluation_type_id")]
        public int TrainingEvaluationTypeId { get; set; }

        //[ForeignKey("TrainingEvaluationTypeId")]
        //public TrainingEvaluationType TrainingEvaluationType { get; set; }

        [Column("score")]
        public decimal Score { get; set; }
        public TrainingEvaluationScore()
        {

        }

        public TrainingEvaluationScore(int trainingEvaluationId,int trainingEvaluationTypeId,decimal score) {
            TrainingEvaluationId = trainingEvaluationId;
            TrainingEvaluationTypeId = trainingEvaluationTypeId;
            Score = score;
        }

        public static  List<TrainingEvaluationScore> Fusion_TrainingEvaluationId_TrainingEvaluationTypeId(int trainingEvaluationId,List<TrainingEvaluationType> trainingEvaluationTypes)
        {
            var scores = trainingEvaluationTypes.Select(typeScore => new TrainingEvaluationScore
            {
                TrainingEvaluationId = trainingEvaluationId,
                TrainingEvaluationTypeId = typeScore.Id,
                Score = typeScore.Rating
            }).ToList();

            return scores;
        }
    }
}
