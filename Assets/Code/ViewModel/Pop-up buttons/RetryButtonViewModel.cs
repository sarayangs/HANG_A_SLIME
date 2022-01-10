using UniRx;

public class RetryButtonViewModel
{
    public readonly ReactiveCommand OnRetryButtonPressed;

    public RetryButtonViewModel()
    {
        OnRetryButtonPressed = new ReactiveCommand();
    }
}