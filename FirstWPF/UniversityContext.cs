using Microsoft.EntityFrameworkCore;


namespace FirstWPF
{
    public class UniversityContext : DbContext 
    {
        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options) 
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local);Database=UniversityDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Group)
                .WithMany(g => g.Students);
        }
    }

}
