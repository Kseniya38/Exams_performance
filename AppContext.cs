using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;


namespace ExamsPerformance
{
    public partial class AppContext : DbContext
    {
        public AppContext() : base("AppContext"){ }
    
        public DbSet<AttestationType> AttestationType { get; set; }
        public DbSet<Attestation> Attestation { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<StudentsInGroup> StudentsInGroup { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
