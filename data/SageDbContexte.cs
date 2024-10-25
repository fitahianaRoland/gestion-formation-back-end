using Microsoft.EntityFrameworkCore;
using gestion_fomation_back_end_local.Models.models;

namespace gestion_fomation_back_end_local.data
{
    public class SageDbContext : DbContext
    {
        public SageDbContext(DbContextOptions<SageDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<EmployeeSage> Employee { get; set; } = default!;
    }
}
