using Core.Entites.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Contexts
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=KJ4FUIB1;Database=HRMS;Trusted_Connection=true");
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateExperience> CandidateComputerExperiences { get; set; }
        public DbSet<CandidateExperince> CandidateExperinces { get; set; }
        public DbSet<CandidateReference> CandidateReferences { get; set; }
        public DbSet<CandidateTrainingOrCourse> CandidateTrainingOrCourses { get; set; }
        public DbSet<JobAdvert> JobAdverts { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
