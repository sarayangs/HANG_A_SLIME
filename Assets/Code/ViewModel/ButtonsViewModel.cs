using UniRx;

public class ButtonsViewModel
{
    public readonly ReactiveCommand HomeButtonPressed;
    public readonly ReactiveCommand ScoreButtonPressed;
    public readonly ReactiveCommand SettingsButtonPressed;

    public readonly ReactiveProperty<bool> HomeIsPressed;
    public readonly ReactiveProperty<bool> ScoreIsPressed;
    public readonly ReactiveProperty<bool> SettingsIsPressed;

    public ButtonsViewModel()
    {
        HomeButtonPressed = new ReactiveCommand();
        ScoreButtonPressed = new ReactiveCommand();
        SettingsButtonPressed = new ReactiveCommand();

        HomeIsPressed = new ReactiveProperty<bool>(true);
        ScoreIsPressed = new ReactiveProperty<bool>(false);
        SettingsIsPressed = new ReactiveProperty<bool>(false);
    }
}