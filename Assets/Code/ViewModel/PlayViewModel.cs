using UniRx;
using UnityEngine;

public class PlayViewModel
{
    public readonly ReactiveCommand<string> KeyPressed;
    public readonly ReactiveProperty<string> HangmanText;
    public readonly ReactiveProperty<string> IncorrectLetters;
    public readonly ReactiveProperty<string> CorrectLetters;
    public readonly ReactiveProperty<string> Health;
    public readonly ReactiveProperty<string> Score;

    public PlayViewModel()
    {
        KeyPressed = new ReactiveCommand<string>();
        HangmanText = new ReactiveProperty<string>();
        IncorrectLetters = new ReactiveProperty<string>(string.Empty);
        CorrectLetters = new ReactiveProperty<string>(string.Empty);
        Health = new ReactiveProperty<string>();
        Score = new ReactiveProperty<string>();
    }

}