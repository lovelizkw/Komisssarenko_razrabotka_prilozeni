namespace RaceData
{
    public class RaceResult
    {
        public long Id { get; set; }
        public string Driver { get; set; } = "";
        public string Car { get; set; } = "";
        public string Track { get; set; } = "";
        public int LapSeconds { get; set; }
    }
}
