namespace SnappetChallenge.Report.Domain
{
    public class ChildResult : Entity
    {
        public long SubmittedAnswerId { get; protected set; }
        public DateTime SubmitDateTime { get; protected set; }
        public long Correct { get; protected set; }
        public int Progress { get; protected set; }
        public long UserId { get; protected set; }
        public long ExerciseId { get; protected set; }
        public string Difficulty { get; protected set; }
        public string Subject { get; protected set; }
        public string Domain { get; protected set; }
        public string LearningObjective { get; protected set; }

        protected ChildResult() { }

        public ChildResult(long submittedAnswerId, DateTime submitDateTime, long correct,
            int progress, long userId, long exerciseId, string difficulty,
            string subject, string domain, string learningObjective)
        {
            SubmittedAnswerId = submittedAnswerId;
            SubmitDateTime = submitDateTime;
            Correct = correct;
            Progress = progress;
            UserId = userId;
            ExerciseId = exerciseId;
            Difficulty = difficulty;
            Subject = subject;
            Domain = domain;
            LearningObjective = learningObjective;
        }


    }
}