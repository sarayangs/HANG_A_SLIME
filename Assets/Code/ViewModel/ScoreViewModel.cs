using UniRx;

public class ScoreViewModel 
{
    public readonly ReactiveProperty<bool> IsVisible;
    public readonly ReactiveCollection<ScoreItemViewModel> Scores;

    public ScoreViewModel()
    {
        IsVisible = new ReactiveProperty<bool>(false);
        Scores = new ReactiveCollection<ScoreItemViewModel>();
    }
}