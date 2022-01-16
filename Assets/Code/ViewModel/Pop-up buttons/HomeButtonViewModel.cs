using UniRx;

public class HomeButtonViewModel
{
    public readonly ReactiveCommand OnHomeButtonPressed;
    
    public HomeButtonViewModel()
    {
        OnHomeButtonPressed = new ReactiveCommand();
    }
}