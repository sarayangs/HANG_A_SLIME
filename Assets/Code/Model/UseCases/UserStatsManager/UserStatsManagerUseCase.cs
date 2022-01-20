using System;
using UnityEngine;

public class UserStatsManagerUseCase : IUserStatsManager
{
    private readonly IAccessUserData _userRepository;
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IRealtimeDatabase _realtimeDatabase;
    private readonly ITimeManager _timeManagerUseCase;
    private readonly IAdmobInitializer _admobInitializer;

    private float _timeInSeconds;

    public UserStatsManagerUseCase(IAccessUserData userRepository, IEventDispatcherService eventDispatcherService,
        IRealtimeDatabase realtimeDatabase,
        ITimeManager timeManagerUseCase, IAdmobInitializer admobInitializer)
    {
        _userRepository = userRepository;
        _eventDispatcherService = eventDispatcherService;
        _realtimeDatabase = realtimeDatabase;
        _timeManagerUseCase = timeManagerUseCase;
        _admobInitializer = admobInitializer;
    }

    public async void ManageUserStats(bool hasWon)
    {
        Debug.Log(_userRepository.GetLocalUser().Score);
        _timeManagerUseCase.FinishTimer();
        _timeInSeconds = _timeManagerUseCase.GetTimer();
        TimeSpan time = TimeSpan.FromSeconds(_timeInSeconds);

        UserEntity user;

        if (hasWon)
        {
            AddUserStats();
        }
        else if (!hasWon && _userRepository.GetLocalUser().GotAnotherChance)
        {
            _admobInitializer.ShowAd();
            return;
        }

        user = _userRepository.GetLocalUser();
        var savedScore = await _realtimeDatabase.GetData($"scores/{_userRepository.GetLocalUser().UserId}/Score");
        if (Int32.Parse(savedScore) < user.Score)
        {
            var entry = new ScoreEntry(user.UserId, user.Score, user.Name);
            TimeSpan accumulatedTime = TimeSpan.FromSeconds(user.Time);
            entry.Time = accumulatedTime.ToString("hh':'mm':'ss");
            _realtimeDatabase.UpdateData(entry);
        }

        _eventDispatcherService.Dispatch<Answer>(new Answer(hasWon, user.Score, time.ToString("hh':'mm':'ss")));
    }

    private void AddUserStats()
    {
        var user = _userRepository.GetLocalUser();
        user.CorrectWords++;
        user.Score += 100 * user.CorrectWords;
        user.Time += _timeInSeconds;
        _userRepository.SetLocalUser(user);
    }
}