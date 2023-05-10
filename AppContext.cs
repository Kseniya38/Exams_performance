using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;


namespace Exams_performance
{
    public partial class AppContext : DbContext
    {
        public AppContext() : base("AppContext")
        {
            //Database.SetInitializer<AppContext>(null);
        }
    
        public DbSet<Attestation_type> Attestation_type { get; set; }
        public DbSet<Attestation> Attestation { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<Students_in_group> Students_in_group { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
