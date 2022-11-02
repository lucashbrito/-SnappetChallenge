using Moq;
using SnappetChallenge.Report.Domain;
using SnappetChallenge.Report.Repository;

namespace SnappetChallenge.Report.UnitTest
{
    public class ReportServicesTests
    {
        private Mock<Repository.IReportRepository> reportRepositoryMock;
        private Service.IReportService reportService;

        public ReportServicesTests()
        {
            reportRepositoryMock = new Mock<Repository.IReportRepository>();
            reportService = new Service.ReportService(reportRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_ImportExternalData()
        {
            reportRepositoryMock.Setup(x => x.HasAnyChildResult(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<long>()))
                .ReturnsAsync(false);

            await reportService.ImportExternalData(new List<ChildResult>()
            {
                new ChildResult(12, DateTime.UtcNow, 1, 1, 1, 1,"difficulty1", "subject1", "domain1", "learningObjective1"),
                new ChildResult(13, DateTime.UtcNow, 2, 21, 21, 21,"difficulty2", "subject2", "domain2", "learningObjective2"),
                new ChildResult(14, DateTime.UtcNow, 3, 31, 31, 31,"difficulty3", "subject3", "domain3", "learningObjective3"),
            });

            reportRepositoryMock.Verify(x => x.Create(It.IsAny<ChildResult>()), Times.Exactly(3));
            reportRepositoryMock.Verify(x => x.HasAnyChildResult(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<long>()), Times.Exactly(3));
            reportRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Exactly(1));
        }

        [Fact]
        public async Task Should_GetUsers()
        {
            var response = new List<long>() { 12, 3, 1 };
            reportRepositoryMock.Setup(x => x.GetUsers()).ReturnsAsync(response);

            var users = await reportService.GetUsers();

            reportRepositoryMock.Verify(x => x.GetUsers(), Times.Exactly(1));
            Assert.Equal(response[0], users[0]);
            Assert.Equal(response[1], users[1]);
            Assert.Equal(response[2], users[2]);
        }

        [Fact]
        public async Task Should_GetSubject()
        {
            var response = new List<string>() { "subject1", "subject2", "subject3" };
            reportRepositoryMock.Setup(x => x.GetSubject()).ReturnsAsync(response);

            var users = await reportService.GetSubject();

            reportRepositoryMock.Verify(x => x.GetSubject(), Times.Exactly(1));
            Assert.Equal(response[0], users[0]);
            Assert.Equal(response[1], users[1]);
            Assert.Equal(response[2], users[2]);
        }

        [Fact]
        public async Task Should_GetLearningObjective()
        {
            var response = new List<string>() { "LearningObjective1", "LearningObjective2", "LearningObjective3" };
            reportRepositoryMock.Setup(x => x.GetLearningObjective()).ReturnsAsync(response);

            var users = await reportService.GetLearningObjective();

            reportRepositoryMock.Verify(x => x.GetLearningObjective(), Times.Exactly(1));
            Assert.Equal(response[0], users[0]);
            Assert.Equal(response[1], users[1]);
            Assert.Equal(response[2], users[2]);
        }

        [Fact]
        public async Task Should_GetOverview()
        {
            var response = new List<DailyOverview>() {
                new DailyOverview(12,  1,  "subject1", "learningObjective1"),
                new DailyOverview(14,  1,  "subject14", "learningObjective14"),
                new DailyOverview(15,  1,  "subject15", "learningObjective15"),
                new DailyOverview(16,  1,  "subject16", "learningObjective16"),
            };

            reportRepositoryMock.Setup(x => x.GetOverview(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<long?>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync((response, response.Count));

            (var dailyOverview, var total) = await reportService.GetOverview(null, null, null, null);

            reportRepositoryMock.Verify(x => x.GetOverview(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<long?>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Exactly(1));

            Assert.True(response[0].Equals(dailyOverview[0]));
            Assert.True(response[1].Equals(dailyOverview[1]));
            Assert.True(response[2].Equals(dailyOverview[2]));
            Assert.Equal(response.Count, total);
        }
    }
}