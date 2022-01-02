using UniRx;

public class PlayViewModel
{
    public readonly ReactiveCommand<string> KeyPressed;
    public readonly ReactiveProperty<string> HangmanText;
    public readonly ReactiveProperty<string> IncorrectLetters;
    public readonly ReactiveProperty<string> CorrectLetters;
    

    public PlayViewModel()
    {
        KeyPressed = new ReactiveCommand<string>();
        HangmanText = new ReactiveProperty<string>();
        IncorrectLetters = new ReactiveProperty<string>(string.Empty);
        CorrectLetters = new ReactiveProperty<string>(string.Empty);
    }

}