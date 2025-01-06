using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

public class SimDbContext : DbContext
{
    public SimDbContext(DbContextOptions<SimDbContext> options)
        : base(options)
    {
    }
    public DbSet<EmployeSA> employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .Property(p => p.Employee_id)
            .ValueGeneratedOnAdd();
    }
}
