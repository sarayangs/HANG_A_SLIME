using Firebase.Analytics;
using UnityEngine;

public class FirebaseAnalyticsService : IAnalyticsService
{
    public void StartLevelEvent(int level)
    {
        Debug.Log($"Start level event: level_{level}");
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart,
                new Parameter(FirebaseAnalytics.ParameterLevelName, "level_"),
                new Parameter( FirebaseAnalytics.ParameterLevel, level)
            );
    }

    public void NewChanceEvent(bool ad)
    {
        throw new System.NotImplementedException();
    }

    public void ShowAdEvent()
    {
        throw new System.NotImplementedException();
    }
}