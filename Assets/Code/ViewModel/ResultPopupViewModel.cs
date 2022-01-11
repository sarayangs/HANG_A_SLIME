using UniRx;
public class ResultPopupViewModel
{
    public readonly ReactiveProperty<bool> Win;
    public readonly ReactiveProperty<bool> IsVisible;

    public ResultPopupViewModel()
    {
        Win = new ReactiveProperty<bool>();
        IsVisible = new ReactiveProperty<bool>(false);
    }
}