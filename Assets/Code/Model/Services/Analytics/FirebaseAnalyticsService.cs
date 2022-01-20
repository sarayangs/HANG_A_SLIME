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
        FirebaseAnalytics.LogEvent("new_chance", new Parameter("view_ad", ad.ToString()));
    }

    public void ShowAdEvent()
    {
        FirebaseAnalytics.LogEvent("show_ad");
    }
}