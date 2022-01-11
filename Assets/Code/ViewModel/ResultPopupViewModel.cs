using UniRx;
using UnityEngine.UI;

public class ResultPopupViewModel
{
    public readonly ReactiveProperty<bool> Win;
    public readonly ReactiveProperty<bool> Lose;
    public readonly ReactiveProperty<bool> IsVisible;
    public readonly ReactiveProperty<string> Score;
    public readonly ReactiveProperty<string> Time;
    public readonly ReactiveProperty<string> Text;

    public ResultPopupViewModel()
    {
        Win = new ReactiveProperty<bool>();
        Lose = new ReactiveProperty<bool>();
        IsVisible = new ReactiveProperty<bool>(false);
        Score = new ReactiveProperty<string>();
        Time = new ReactiveProperty<string>();
        Text = new ReactiveProperty<string>();
    }
}