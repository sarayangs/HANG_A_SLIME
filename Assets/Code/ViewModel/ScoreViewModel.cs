using UniRx;

public class ScoreViewModel 
{
    public readonly ReactiveProperty<bool> IsVisible;

    public ScoreViewModel()
    {
        IsVisible = new ReactiveProperty<bool>();
    }
}