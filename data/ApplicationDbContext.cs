using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Departement> Departements { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Trainer> Trainers { get; set; } 
    public DbSet<Training> trainings { get; set; }
    public DbSet<Trainer_Type> trainer_Types { get; set; }
    public DbSet<TrainingOrganization> trainingOrganizations { get; set; }
    public DbSet<Session> session { get; set; }
    public DbSet<Employee> employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Departement>()
            .Property(p => p.Id)
            .HasDefaultValueSql($"NEXT VALUE FOR dept_seq");

        modelBuilder.Entity<Admin>()
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
    }
}
