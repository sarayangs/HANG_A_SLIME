using System.Threading.Tasks;

public interface IRealtimeDatabase 
{
     void AddData(ScoreEntry entry);
     Task<string> GetData(string userId);
     void UpdateData(ScoreEntry entry);
     void GetScores();
}