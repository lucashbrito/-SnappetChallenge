namespace SnappetChallenge.Report.API.Controllers.Report.v1.Model
{
    public class ChildResultRequestV1
    {
        public long SubmittedAnswerId { get; set; }
        public string SubmitDateTime { get; set; }
        public long Correct { get; set; }
        public int Progress { get; set; }
        public long UserId { get; set; }
        public long ExerciseId { get; set; }
        public string Difficulty { get; set; }
        public string Subject { get; set; }
        public string Domain { get; set; }
        public string LearningObjective { get; set; }
    }
}
