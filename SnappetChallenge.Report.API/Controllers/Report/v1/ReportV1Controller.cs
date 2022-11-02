using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SnappetChallenge.Report.API.Controllers.Report.v1.Model;
using SnappetChallenge.Report.Domain;
using SnappetChallenge.Report.Domain.DomainObjects;
using SnappetChallenge.Report.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace SnappetChallenge.Report.API.Controllers.Report.v1
{
    [ApiController]
    [Route("report")]
    public class ReportV1Controller : ControllerBase
    {
        private IReportService reportService;
        private readonly IMapper mapper;

        public ReportV1Controller(IReportService reportService, IMapper mapper)
        {
            this.reportService = reportService;
            this.mapper = mapper;
        }

        [HttpPost("ImportExternalData", Name = "ImportExternalData"), DisableRequestSizeLimit]
        public async Task<IActionResult> ImportExternalData([FromBody] List<ChildResultRequestV1> request)
        {
            await reportService.ImportExternalData(mapper.Map<List<ChildResultRequestV1>, List<ChildResult>>(request));

            return Ok();
        }

        [HttpGet("overview", Name = "GetOverview")]
        public async Task<IActionResult> GetUserIdOverview([FromQuery] int? page, int? pageSize,
           long? userIds, DateTime? date)
        {
            (var overwiew, var total) = await reportService.GetOverview(page, pageSize, userIds, date);

            return Ok(new PagedResult<DailyOverview>(overwiew, total, page, pageSize));
        }

    }
}
