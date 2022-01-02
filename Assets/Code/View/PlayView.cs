using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using DG.Tweening;

public class PlayView : View
{ 
    [SerializeField] private TextMeshProUGUI _hangmanText;
    [SerializeField] private TextMeshProUGUI _correctLettersText;
    [SerializeField] private TextMeshProUGUI _incorrectLettersText;

    [SerializeField] private List<Button> _keyboard;

    private PlayViewModel _viewModel;

    public void SetUp(PlayViewModel viewModel)
    {
        _viewModel = viewModel;

        foreach (var key in _keyboard)
        {
            key.onClick.AddListener(() =>
            {
                _viewModel.KeyPressed.Execute(key.GetComponentInChildren<TMP_Text>().text);
                key.GetComponent<Image>().DOColor(new Color(0.5f, 0.5f ,0.5f), 0.2f);
                key.enabled = false;
            });
        }

        _viewModel.HangmanText.Subscribe(text =>
        {
            _hangmanText.SetText(text);
        });

        _viewModel.CorrectLetters.Subscribe(text =>
        {
            _correctLettersText.SetText(text);
        });
        
        _viewModel.IncorrectLetters.Subscribe(text =>
        {
            _incorrectLettersText.SetText(text);
        });
    }
}