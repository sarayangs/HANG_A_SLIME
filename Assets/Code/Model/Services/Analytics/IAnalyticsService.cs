public interface IAnalyticsService
{
    void StartLevelEvent(int level);
    void NewChanceEvent(bool ad);
    void ShowAdEvent();
}