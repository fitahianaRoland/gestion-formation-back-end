using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Departement> Departements { get; set; }
    public DbSet<AppUser> AppUser { get; set; }
    public DbSet<Training> trainings { get; set; }
    public DbSet<Trainer_Type> trainer_Types { get; set; }
    public DbSet<Session> session { get; set; }
    public DbSet<Employee> employees { get; set; }
    public DbSet<Training_request> training_Requests { get; set; }
    public DbSet<State> states { get; set; }
    public DbSet<ExternalTraining> externe { get; set; }
    public DbSet<InternalTraining> interne { get; set; }
    public DbSet<TrainerOrganization> trainerOrganizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Departement>()
            .Property(p => p.Id)
            .HasDefaultValueSql($"NEXT VALUE FOR dept_seq");

        modelBuilder.Entity<AppUser>()
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

        modelBuilder.Entity<Training_request>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();

        modelBuilder.Entity<State>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();

        modelBuilder.Entity<ExternalTraining>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();

        modelBuilder.Entity<InternalTraining>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();

        modelBuilder.Entity<TrainerOrganization>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();
    }
}
