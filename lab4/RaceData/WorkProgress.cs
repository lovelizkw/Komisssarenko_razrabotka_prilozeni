namespace RaceData
{
    public class WorkProgress
    {
        public WorkProgress(string stage, long done, long total)
        {
            Stage = stage;
            Done = done;
            Total = total;
        }

        public string Stage { get; }
        public long Done { get; }
        public long Total { get; }

        public long Percent => Total > 0 ? 100L * Done / Total : 0;
    }
}
