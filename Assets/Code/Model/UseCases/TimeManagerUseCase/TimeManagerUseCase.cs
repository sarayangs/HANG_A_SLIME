using TMPro.EditorUtilities;
using UnityEngine;

public class TimeManagerUseCase : ITimeManager
{
    private readonly IEventDispatcherService _eventDispatcherService;
    
    private float _startTime;
    private float _time;
    private float _startPause;
    private float _pausedTime;

    public TimeManagerUseCase(IEventDispatcherService eventDispatcherService)
    {
        _eventDispatcherService = eventDispatcherService;
        _pausedTime = 0;
    }
    
    public void StartTimer()
    {
        _startTime = Time.realtimeSinceStartup;
    }

    public void FinishTimer()
    {
        _time = Time.realtimeSinceStartup - _startTime - _pausedTime;
    }

    public void StopTimer()
    {
        _startPause = Time.realtimeSinceStartup;
    }

    public void ResumeTimer()
    {
        _pausedTime += Time.realtimeSinceStartup - _startPause;
        _eventDispatcherService.Dispatch<float>(_pausedTime);
    }

    public float GetTimer()
    {
        return _time;
    }
}