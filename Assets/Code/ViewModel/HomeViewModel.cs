using UniRx;

public class HomeViewModel
{
    public readonly ReactiveProperty<bool> IsVisible;
    public readonly ReactiveCommand PlayButtonPressed;
    public readonly ReactiveCommand ChangeNameButtonPressed;
    public readonly ReactiveProperty<string> Name;
    

    public HomeViewModel()
    {
        IsVisible = new ReactiveProperty<bool>(true);
        PlayButtonPressed = new ReactiveCommand();
        ChangeNameButtonPressed = new ReactiveCommand();
        Name = new ReactiveProperty<string>();
    }
}