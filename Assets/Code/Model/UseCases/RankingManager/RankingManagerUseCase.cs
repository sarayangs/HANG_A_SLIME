using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class RankingManagerUseCase : IRankingManager
{
    private readonly IRealtimeDatabase _realtimeDatabaseService;
    private readonly IEventDispatcherService _eventDispatcherService;
    private List<RankingEntry> arrangedUsers;
    
    public RankingManagerUseCase(IRealtimeDatabase realtimeDatabaseService, IEventDispatcherService eventDispatcherService)
    {
        arrangedUsers = new List<RankingEntry>();
        _realtimeDatabaseService = realtimeDatabaseService;
        _eventDispatcherService = eventDispatcherService;
    }
    public async void GetAllData()
    {
       var users = await _realtimeDatabaseService.GetScoreList();
       ArrangeByScore(users);
    }

    private void ArrangeByScore(List<ScoreEntry> users)
    {
        var sortedUsers = users.OrderByDescending(x => x.Score).ToList();

        for (int i = 0; i < sortedUsers.Count; i++)
        {
            var index = i + 1;
            var entry = new RankingEntry(index.ToString(), sortedUsers[i].Name,
                sortedUsers[i].Score.ToString(), sortedUsers[i].Time.ToString());
            arrangedUsers.Add(entry);
            
            _eventDispatcherService.Dispatch<RankingEntry>(entry);
        }
    }

    public void Send()
    {
        throw new System.NotImplementedException();
    }
}