using SnappetChallenge.Report.Domain;
using SnappetChallenge.Report.Repository;

namespace SnappetChallenge.Report.Service
{
    public class ReportService : IReportService
    {
        private IReportRepository reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public async Task ImportExternalData(List<ChildResult> externalData)
        {
            var count = 0;

            foreach (var childResult in externalData)
            {
                if (await reportRepository.HasAnyChildResult(childResult.UserId, childResult.ExerciseId, childResult.SubmittedAnswerId))
                {
                    return;
                }

                reportRepository.Create(childResult);
                count++;
                if (count == 100)
                {
                    await reportRepository.SaveChangesAsync();
                    count = 0;
                }
                else if (externalData.Count < 100 && count == externalData.Count)
                {
                    await reportRepository.SaveChangesAsync();
                }
            }
        }


        public async Task<List<long>> GetUsers()
        {
            return await reportRepository.GetUsers();
        }
        public async Task<List<string>> GetSubject()
        {
            return await reportRepository.GetSubject();
        }

        public async Task<(List<DailyOverview>, int total)> GetOverview(int? page, int? pageSize, long? userIds, DateTime? date)
        {
            var now = date == null ? new DateTime(2015, 03, 24, 11, 30, 00) : date;
            var start = new DateTime(now.GetValueOrDefault().Year, now.GetValueOrDefault().Month, now.GetValueOrDefault().Day);

            (var overview, var total) = await reportRepository.GetOverview(page, pageSize, userIds, start, now.GetValueOrDefault());

            return (overview, total);
        }

        public async Task<List<string>> GetLearningObjective()
        {
            return await reportRepository.GetLearningObjective();
        }
    }
}