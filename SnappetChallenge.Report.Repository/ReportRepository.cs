using Microsoft.EntityFrameworkCore;
using SnappetChallenge.Report.Domain;

namespace SnappetChallenge.Report.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly ReportDatabaseContext databaseContext;
        public int PageSize { get; set; } = 500;
        public int PageNumber { get; set; } = 1;

        public ReportRepository(ReportDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Create(Domain.ChildResult childResult)
        {
            databaseContext.ChildResults.Add(childResult);
        }

        public async Task<bool> HasAnyChildResult(long userId, long exerciseId, long submittedAnswerId)
        {
            return await databaseContext.ChildResults
                         .AnyAsync(x => x.UserId == userId && x.ExerciseId == exerciseId && x.SubmittedAnswerId == submittedAnswerId);
        }

        public async Task<List<string>> GetLearningObjective()
        {
            return await databaseContext.ChildResults.Distinct().Select(x => x.LearningObjective).ToListAsync();
        }
        public async Task<List<long>> GetUsers()
        {
            return await databaseContext.ChildResults.Distinct().Select(x => x.UserId).ToListAsync();
        }

        public async Task<List<string>> GetSubject()
        {
            return await databaseContext.ChildResults.Distinct().Select(x => x.Subject).ToListAsync();
        }

        private int GetPageSize(int? pageSize)
        {
            return pageSize == null || pageSize.Value <= 0 ? PageSize : pageSize.GetValueOrDefault();
        }

        private int GetPage(int? page)
        {
            return page == null || page.Value <= 0 ? PageNumber : page.GetValueOrDefault();
        }

        public async Task<(List<DailyOverview>, int total)> GetOverview(int? page, int? pageSize,
            long? userIds, DateTime start, DateTime end)
        {
            var overview = await databaseContext.ChildResults
                .Where(x => (userIds != null ? userIds == x.UserId : true)
                       && x.SubmitDateTime >= start && x.SubmitDateTime <= end)
                .GroupBy(x => new { x.UserId, x.Subject, x.LearningObjective })
                .Select(x => new DailyOverview(x.Sum(y => y.Progress), x.Key.UserId, x.Key.Subject, x.Key.LearningObjective))
                .Skip(GetPageSize(pageSize) * (GetPage(page) - 1))
                .Take(GetPageSize(pageSize))
                .ToListAsync();

            var total = databaseContext.ChildResults
                 .Where(x => (userIds != null ? userIds == x.UserId : true)
                        && x.SubmitDateTime >= start && x.SubmitDateTime <= end)
                 .GroupBy(x => new { x.UserId, x.Subject, x.LearningObjective }).Count();

            return (overview, total);
        }

        public async Task SaveChangesAsync()
        {
            await databaseContext.SaveChangesAsync();
        }
    }
}