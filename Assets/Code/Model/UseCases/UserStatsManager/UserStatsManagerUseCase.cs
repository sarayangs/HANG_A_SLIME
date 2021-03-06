using System;
using System.Web.UI.WebControls;
using UnityEngine;

public class UserStatsManagerUseCase : IUserStatsManager
{
    private readonly IAccessUserData _userRepository;
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IRealtimeDatabase _realtimeDatabase;
    private readonly ITimeManager _timeManagerUseCase;
    private readonly IAdmobInitializer _admobInitializer;
    private readonly ISoundHandler _soundHandlerUseCase;

    private float _timeInSeconds;

    public UserStatsManagerUseCase(IAccessUserData userRepository, IEventDispatcherService eventDispatcherService,
        IRealtimeDatabase realtimeDatabase,
        ITimeManager timeManagerUseCase, IAdmobInitializer admobInitializer, ISoundHandler soundHandler)
    {
        _userRepository = userRepository;
        _eventDispatcherService = eventDispatcherService;
        _realtimeDatabase = realtimeDatabase;
        _timeManagerUseCase = timeManagerUseCase;
        _admobInitializer = admobInitializer;
        _soundHandlerUseCase = soundHandler;
    }

    public async void ManageUserStats(bool hasWon)
    {
        _timeManagerUseCase.FinishTimer();
        _timeInSeconds = _timeManagerUseCase.GetTimer();
        TimeSpan time = TimeSpan.FromSeconds(_timeInSeconds);

        UserEntity user;

        if (hasWon)
        {
            _soundHandlerUseCase.Play("victory");
            AddUserStats();
        }
        else if (!hasWon && _userRepository.GetLocalUser().GotAnotherChance)
        {
            _admobInitializer.ShowAd();
            return;
        }
        else
        {
            _soundHandlerUseCase.Play("gameover");
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