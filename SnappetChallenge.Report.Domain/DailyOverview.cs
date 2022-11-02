using SnappetChallenge.Report.Domain.DomainObjects;

namespace SnappetChallenge.Report.Domain
{
    public class DailyOverview : ValueObject<DailyOverview>
    {
        public int Progress { get; protected set; }
        public long UserId { get; protected set; }
        public string Subject { get; protected set; }
        public string LearningObjective { get; protected set; }

        public DailyOverview(int progress, long userId, string subject, string learningObjective)
        {
            Progress = progress;
            UserId = userId;
            Subject = subject;
            LearningObjective = learningObjective;
        }

        public override bool Equals(object? obj)
        {
            return obj is DailyOverview overwiew &&
                   EqualsCore(overwiew);
        }

        protected override bool EqualsCore(DailyOverview other)
        {
            return other.UserId == UserId
                && other.Subject == Subject
                && other.Progress == Progress
                && other.LearningObjective == LearningObjective;
        }
    }
}
