using SnappetChallenge.Report.Domain;

namespace SnappetChallenge.Report.Repository
{
    public interface IReportRepository
    {
        void Create(Domain.ChildResult childResult);
        Task<bool> HasAnyChildResult(long userId, long exerciseId, long submittedAnswerId);
        Task<List<long>> GetUsers();
        Task<List<string>> GetSubject();
        Task<(List<DailyOverview>, int total)> GetOverview(int? page, int? pageSize, long? userIds, DateTime start, DateTime end);
        Task SaveChangesAsync();
        Task<List<string>> GetLearningObjective();
    }
}
