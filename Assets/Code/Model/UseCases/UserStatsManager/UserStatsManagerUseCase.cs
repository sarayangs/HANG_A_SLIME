﻿using System;
using UnityEngine;

public class UserStatsManagerUseCase : IUserStatsManager
{
    private readonly IAccessUserData _userRepository;
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IRealtimeDatabase _realtimeDatabase;
    private readonly ITimeManager _timeManagerUseCase;

    private float _timeInSeconds;
    public UserStatsManagerUseCase(IAccessUserData userRepository, IEventDispatcherService eventDispatcherService, IRealtimeDatabase realtimeDatabase,
    ITimeManager timeManagerUseCase)
    {
        _userRepository = userRepository;
        _eventDispatcherService = eventDispatcherService;
        _realtimeDatabase = realtimeDatabase;
        _timeManagerUseCase = timeManagerUseCase;
    }

    public async void ManageUserStats(bool hasWon)
    {
        _timeManagerUseCase.FinishTimer();
        _timeInSeconds = _timeManagerUseCase.GetTimer();
        TimeSpan time = TimeSpan.FromSeconds(_timeInSeconds);
        
        UserEntity user;
      
        if (hasWon)
        {
            AddUserStats();
         }
        else
        {
            
        }
        user = _userRepository.GetLocalUser();
        var savedScore = await _realtimeDatabase.GetData($"scores/{_userRepository.GetLocalUser().UserId}/Score");
        Debug.Log(Int32.Parse(savedScore));
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