using Microsoft.AspNetCore.Mvc.RazorPages;
using SnappetChallenge.Report.Domain;
using SnappetChallenge.Report.Domain.DomainObjects;
using SnappetChallenge.Report.Service;

namespace SnappetChallenge.Report.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IReportService reportService;

        public PagedResult<DailyOverview> DailyOverview;
        public List<long> UserUids;
        public List<string> Subsjects;
        public List<string> LearningObjectives;

        public string NameSort { get; set; }
        public string DateSort { get; set; }

        public long? UserId { get; set; }
        public DateTime? Date { get; set; }

        public IndexModel(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public async Task OnGet(long? userId, DateTime? date, int? page, int? pageSize)
        {
            ////TODO I would create a nice view with the options to make some filters and make easy for the teachers. 
            ///give them the options to filter per subjects, users, or however they would like. 
            
            Subsjects = await reportService.GetSubject();
            UserUids = await reportService.GetUsers();
            LearningObjectives = await reportService.GetLearningObjective();

            (var overview, var total) = await reportService.GetOverview(page, pageSize, userId, date);

            DailyOverview = new PagedResult<DailyOverview>(overview, total, page, pageSize);
        }
    }
}