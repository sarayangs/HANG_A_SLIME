using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRealtimeDatabase 
{
     void AddData(ScoreEntry entry);
     Task<string> GetData(string userId);
     void UpdateData(ScoreEntry entry);
     void GetScores();
     Task<List<ScoreEntry>> GetScoreList();
}