using Microsoft.EntityFrameworkCore;

namespace Wasla.Models
{
    public class WaslaContext : DbContext
    {
        public WaslaContext(DbContextOptions<WaslaContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Recruiter> Recruiters { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Application> Applies { get; set; }
        public DbSet<ApplicationHistory> ApplicationHistories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<CoinHistory> CoinHistories { get; set; }

        public DbSet<Degree> DegreeTypes { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<ProcessType> ProcessTypes { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasKey(a => new { a.EmployeeId, a.JobId });

            modelBuilder.Entity<ApplicationHistory>()
                .HasKey(ah => new { ah.EmployeeId, ah.JobId, ah.ChangeDate });

            modelBuilder.Entity<CoinHistory>()
                .HasKey(ch => new { ch.UserId, ch.Time });

            modelBuilder.Entity<Education>().
                HasKey(e => new { e.EmployeeId, e.StartDate });

            modelBuilder.Entity<Experience>().
                HasKey(e => new { e.EmployeeId, e.StartDate });

            modelBuilder.Entity<Feedback>()
                .HasKey(f => new { f.SenderId, f.ReceiverId, f.Time });

            modelBuilder.Entity<EmployeeSkill>().
                HasKey(es => new { es.EmployeeId, es.SkillId });

            // TPT configuration
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Recruiter>().ToTable("Recruiters");
            modelBuilder.Entity<User>().ToTable("Users");
        }


    }
}
