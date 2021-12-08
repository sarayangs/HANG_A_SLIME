public class RankingEntry
{
    public string Position;
    public string Name;
    public string Score;
    public string Time;

    public RankingEntry(string position, string name, string score, string time)
    {
        Position = position;
        Name = name;
        Score = score;
        Time = time;
    }
}