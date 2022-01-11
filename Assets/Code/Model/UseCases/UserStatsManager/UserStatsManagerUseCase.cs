using System;
using UnityEngine;

public class UserStatsManagerUseCase : IUserStatsManager
{
    private readonly IAccessUserData _userRepository;
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IRealtimeDatabase _realtimeDatabase;

    public UserStatsManagerUseCase(IAccessUserData userRepository, IEventDispatcherService eventDispatcherService, IRealtimeDatabase realtimeDatabase)
    {
        _userRepository = userRepository;
        _eventDispatcherService = eventDispatcherService;
        _realtimeDatabase = realtimeDatabase;
    }

    public async void ManageUserStats(bool hasWon)
    {
        UserEntity user;
        
        if (hasWon)
        {
            AddScore();
            user = _userRepository.GetLocalUser();
            var savedScore = await _realtimeDatabase.GetData($"scores/{_userRepository.GetLocalUser().UserId}/Score");
            
            if (Int32.Parse(savedScore) < user.Score)
            {
                var entry = new ScoreEntry(user.UserId, user.Score, user.Name);
                _realtimeDatabase.UpdateData(entry);
            }
        }
        else
        {
            
        }

        user = _userRepository.GetLocalUser();
        _eventDispatcherService.Dispatch<Answer>(new Answer(hasWon, user.Score, 0));

    }

    private void AddScore()
    {
        var user = _userRepository.GetLocalUser();
        user.CorrectWords++;
        user.Score += 100 * user.CorrectWords;
        _userRepository.SetLocalUser(user);
    }
}