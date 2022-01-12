using TMPro.EditorUtilities;
using UnityEngine;

public class TimeManagerUseCase : ITimeManager
{
    private float _startTime;
    private float _time;
    
    public void StartTimer()
    {
        _startTime = Time.realtimeSinceStartup;
    }

    public void FinishTimer()
    {
        _time = Time.realtimeSinceStartup - _startTime;
    }

    public float GetTimer()
    {
        return _time;
    }
}