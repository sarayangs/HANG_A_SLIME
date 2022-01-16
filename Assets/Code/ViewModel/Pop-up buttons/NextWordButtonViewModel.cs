using UniRx;

public class NextWordButtonViewModel
{
    public readonly ReactiveCommand OnNextWordButtonPressed;

    public NextWordButtonViewModel()
    {
        OnNextWordButtonPressed = new ReactiveCommand();
    }
}