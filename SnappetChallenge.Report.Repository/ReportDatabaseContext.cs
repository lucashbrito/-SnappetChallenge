using Microsoft.EntityFrameworkCore;

namespace SnappetChallenge.Report.Repository
{
    public class ReportDatabaseContext : DbContext
    {
        public ReportDatabaseContext(DbContextOptions<ReportDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Domain.ChildResult> ChildResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ChildResultMapping(modelBuilder);
        }

        private static void ChildResultMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.ChildResult>()
            .HasKey(m => m.Id);

            modelBuilder.Entity<Domain.ChildResult>()
            .Property(m => m.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Domain.ChildResult>()
            .Property(m => m.ExerciseId)
            .IsRequired();

            modelBuilder.Entity<Domain.ChildResult>()
            .Property(m => m.SubmittedAnswerId)
            .IsRequired();

            modelBuilder.Entity<Domain.ChildResult>()
            .Property(m => m.UserId)
            .IsRequired();

            modelBuilder.Entity<Domain.ChildResult>()
            .Property(m => m.SubmitDateTime)
            .IsRequired();

            modelBuilder.Entity<Domain.ChildResult>()
            .Property(m => m.Subject)
            .HasMaxLength(100);

            modelBuilder.Entity<Domain.ChildResult>()
            .Property(m => m.Difficulty)
            .HasMaxLength(100);

            modelBuilder.Entity<Domain.ChildResult>()
            .Property(m => m.Domain)
            .HasMaxLength(100);

            modelBuilder.Entity<Domain.ChildResult>()
            .Property(m => m.LearningObjective)
            .HasMaxLength(100);
        }

    }
}
