using GestionFormation.Models.classes;
using GestionFormation.Models.Classes;
using Microsoft.EntityFrameworkCore;
using static GestionFormation.Models.repository.ForecastPresenceRepository;
using static GestionFormation.Repository.TrainingEvaluationRepository;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Department> Departements { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Trainer> Trainers { get; set; } 
    public DbSet<Training> trainings { get; set; }
    public DbSet<Trainer_Type> trainer_Types { get; set; }
    public DbSet<TrainingOrganization> trainingOrganizations { get; set; }
    public DbSet<Session> session { get; set; }
    public DbSet<Employee> employees { get; set; }
    public DbSet<UserRoleView> userRoleView { get; set; }
    public DbSet<ForecastPresence> forecastPresence { get; set; }
    public DbSet<AppUser> AppUser { get; set; }
    public DbSet<TrainingEvaluationType> TrainingEvaluationTypes { get; set; }
    public DbSet<ViewTrainingEvaluationStatus> viewTrainingEvaluationStatus { get; set; }
    public DbSet<ViewTrainingSessionEvaluationStatus> viewTrainingSessionEvaluationStatus { get; set; }
    public DbSet<ItemTrainingEvaluation> itemTrainingEvaluation { get; set; }
    public DbSet<TrainingEvaluation> trainingEvaluation { get; set; }
    public DbSet<TrainingEvaluationScore> trainingEvaluationScore { get; set; }
    public DbSet<TrainingEvaluationStatus> trainingEvaluationStatus { get; set; }
    public DbSet<SendingStatus> sendingStatus { get; set; }
    public DbSet<EvaluationToken> evaluationToken { get; set; }
    public DbSet<Profil> profiles { get; set; }
    public DbSet<RoleProfile> roleProfiles { get; set; }
    public DbSet<Access> accesses { get; set; }
    public DbSet<AccessProfile> accessProfiles { get; set; }
    public DbSet<ViewAccessProfile> viewAccessProfiles { get; set; }
    public DbSet<ViewRoleProfile> viewRoleProfiles { get; set; }
    public DbSet<Role> roles { get; set; }
    public DbSet<ViewTrainingEvaluationAverageScore> viewTrainingEvaluationAverageScores { get; set; }
    public DbSet<ViewTrainingEvaluationGeneralAverageScore> viewTrainingEvaluationGeneralAverageScores { get; set; }
    public DbSet<ViewTrainingEvaluationResponseCount> viewTrainingEvaluationResponseCounts { get; set; }
    public DbSet<ViewTrainingPlannedStatus> viewTrainingPlannedStatus { get; set; }
    public DbSet<ViewTrainingSessionPlannedStatus> viewTrainingSessionPlannedStatuses { get; set; }
    public DbSet<ViewTrainingCompletedStatus> viewTrainingCompletedStatuses { get; set; }

    public DbSet<ViewTrainingSessionCompletedStatus> viewTrainingSessionCompletedStatuses { get; set; }
    public DbSet<TrainingSessionStatus> trainingSessionStatus { get; set; }
    public DbSet<ViewTrainingSessionPlannedForCalendar> viewTrainingSessionPlannedForCalendar { get; set; }
    public DbSet<ViewTrainingSessionCompletedForCalendar> viewTrainingSessionCompletedForCalendar { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Department>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Admin>()
          .Property(p => p.Id)
          .ValueGeneratedOnAdd();

        modelBuilder.Entity<AppUser>()
          .Property(p => p.Id)
          .ValueGeneratedOnAdd();

        modelBuilder.Entity<Trainer>()
          .Property(p => p.Id)
          .ValueGeneratedOnAdd();

        modelBuilder.Entity<TrainingOrganization>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();

        modelBuilder.Entity<Trainer_Type>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Training>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Session>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Employee>()
            .Property(p => p.Employee_id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<ForecastPresence>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<TrainingEvaluationType>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
            
        modelBuilder.Entity<UserRoleView>()
            .HasNoKey();

        modelBuilder.Entity<ViewTrainingEvaluationStatus>()
            .HasNoKey();
        modelBuilder.Entity<ViewTrainingSessionEvaluationStatus>()
            .HasNoKey();
        modelBuilder.Entity<ItemTrainingEvaluation>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();
        modelBuilder.Entity<TrainingEvaluation>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();
        modelBuilder.Entity<TrainingEvaluationScore>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<SendingStatus>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Profil>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<RoleProfile>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Access>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<AccessProfile>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Role>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<ViewAccessProfile>()
            .HasNoKey();
        modelBuilder.Entity<ViewRoleProfile>()
            .HasNoKey();
        modelBuilder.Entity<ViewTrainingEvaluationAverageScore>()
            .HasNoKey();
        modelBuilder.Entity<ViewTrainingEvaluationGeneralAverageScore>()
            .HasNoKey();
        modelBuilder.Entity<ViewTrainingEvaluationResponseCount>()
            .HasNoKey();
        modelBuilder.Entity<ViewTrainingPlannedStatus>()
            .HasNoKey();
        modelBuilder.Entity<ViewTrainingSessionPlannedStatus>()
            .HasNoKey();
        modelBuilder.Entity<ViewTrainingCompletedStatus>()
            .HasNoKey();
        modelBuilder.Entity<ViewTrainingSessionCompletedStatus>()
            .HasNoKey();
        modelBuilder.Entity<TrainingSessionStatus>()
            .HasNoKey();
        modelBuilder.Entity<TotalTrainingSessionNumber>().HasNoKey();
        modelBuilder.Entity<TrainingSessionPlannedNumber>().HasNoKey();
        modelBuilder.Entity<TrainingSessionCompletedNumber>().HasNoKey();
        modelBuilder.Entity<GlobalPresenceRate>().HasNoKey();
        modelBuilder.Entity<EvaluationGeneralAverageScore>().HasNoKey();
        modelBuilder.Entity<ViewTrainingSessionPlannedForCalendar>().HasNoKey();
        modelBuilder.Entity<ViewTrainingSessionCompletedForCalendar>().HasNoKey();
    }
}
