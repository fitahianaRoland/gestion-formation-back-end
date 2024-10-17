using Microsoft.EntityFrameworkCore;
using gestion_fomation_back_end_local.Models.models;

namespace gestion_fomation_back_end_local.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Administrateur> administrateur { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Department> Department { get; set; } = default!;
        public DbSet<Employee> Employee { get; set; } = default!;
        public DbSet<Training> Training { get; set; } = default!;
        public DbSet<TrainingSession> TrainingSession { get; set; } = default!;

        // Déclarez vos DbSet ici. Par exemple, si vous avez une entité "Produit" ;
    }   
}
