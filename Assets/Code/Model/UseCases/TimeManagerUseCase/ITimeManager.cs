public interface ITimeManager
{
    void StartTimer();
    void FinishTimer();
    void StopTimer();
    void ResumeTimer();
    float GetTimer();
}