using UniRx;
public class PauseViewModel
{
    public readonly ReactiveCommand OnResumeButtonPressed;
    public readonly ReactiveProperty<bool> IsVisible;

    public PauseViewModel()
    {
        OnResumeButtonPressed = new ReactiveCommand();
        IsVisible = new ReactiveProperty<bool>(false);
    }
}