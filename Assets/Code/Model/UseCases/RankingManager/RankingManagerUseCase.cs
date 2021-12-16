using System.Collections.ObjectModel;

public class RankingManagerUseCase : IRankingManager
{
    private readonly IRealtimeDatabase _realtimeDatabaseService;

    public RankingManagerUseCase(IRealtimeDatabase realtimeDatabaseService)
    {
        _realtimeDatabaseService = realtimeDatabaseService;
    }
    public void GetAllData()
    {
        _realtimeDatabaseService.GetScores();
    }

    public void ArrangeByScore()
    {
        throw new System.NotImplementedException();
    }

    public void Send()
    {
        throw new System.NotImplementedException();
    }
}