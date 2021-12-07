using UniRx;

public class ChangeNameViewModel
{
    public readonly ReactiveCommand<string> SaveButtonPressed;
    public readonly ReactiveProperty<string> NewName;
    public readonly ReactiveProperty<bool> IsVisible;

    public ChangeNameViewModel()
    {
        SaveButtonPressed = new ReactiveCommand<string>();
        NewName = new ReactiveProperty<string>();
        IsVisible = new ReactiveProperty<bool>(false);
    }
}
