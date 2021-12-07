using UniRx;

public class HomeViewModel
{
    public readonly ReactiveProperty<bool> IsVisible;

    public HomeViewModel()
    {
        IsVisible = new ReactiveProperty<bool>(true);
    }
}