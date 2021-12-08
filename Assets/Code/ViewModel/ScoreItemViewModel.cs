using UniRx;

public class ScoreItemViewModel
{
    public readonly ReactiveProperty<string> Position;
    public readonly ReactiveProperty<string> Name;
    public readonly ReactiveProperty<string> Score;
    public readonly ReactiveProperty<string> Time;

    public ScoreItemViewModel(string position, string name, string score, string time)
    {
        Position = new ReactiveProperty<string>(position);
        Name = new ReactiveProperty<string>(name);
        Score = new ReactiveProperty<string>(score);
        Time = new ReactiveProperty<string>(time);
    }
}