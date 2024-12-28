using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWPF
{
    public class UniversityContext:DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) 
            : base(options) 
        {
            Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-4CPJD5R;Database=UniversityDb;Trusted_Connection=True;                       TrustServerCertificate=True;");
            optionsBuilder.ConfigureWarnings(warnings =>
    warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        public DbSet<University> Universities { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
    }

}
