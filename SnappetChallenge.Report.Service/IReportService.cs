using SnappetChallenge.Report.Domain;

namespace SnappetChallenge.Report.Service
{
    public interface IReportService
    {
        Task ImportExternalData(List<ChildResult> externalData);
        Task<List<long>> GetUsers();
        Task<List<string>> GetSubject();
        Task<List<string>> GetLearningObjective();
        Task<(List<DailyOverview>, int total)> GetOverview(int? page, int? pageSize, long? userIds, DateTime? date);
    }
}