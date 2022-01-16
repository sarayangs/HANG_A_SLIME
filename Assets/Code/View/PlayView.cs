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
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private RectTransform _hangmanTransform;
    [SerializeField] private Button _pauseButton;

    [SerializeField] private List<Button> _keyboard;
    [SerializeField] private List<GameObject> _hangmanPrefabs;

    private PlayViewModel _viewModel;
    private int _hangmanIndex;
    private Sequence _sequence;

    public void SetUp(PlayViewModel viewModel)
    {
        _hangmanIndex = 0;
        _viewModel = viewModel;

        foreach (var key in _keyboard)
        {
            key.onClick.AddListener(() =>
            {
                _viewModel.KeyPressed.Execute(key.GetComponentInChildren<TMP_Text>().text);
                key.GetComponent<Image>().DOColor(new Color(0.5f, 0.5f, 0.5f), 0.2f);
                key.enabled = false;
            });
        }

        _pauseButton.onClick.AddListener(() => { _viewModel.OnPauseButtonPressed.Execute(); });

        _viewModel.HangmanText.Subscribe(text => { _hangmanText.SetText(text); });

        _viewModel.CorrectLetters.Subscribe(text => { _correctLettersText.SetText(text); });

        _viewModel.IncorrectLetters.Subscribe(text =>
        {
            _incorrectLettersText.SetText(text);
        });

        _viewModel.OnIncorrectLetter.Subscribe(instantiate =>
        {
            if(instantiate != 0)
            {
                InstantiateHangman();
                PlaySequence();
            }
        });

        _viewModel.Score.Subscribe(score => { _scoreText.SetText(score); });

        _viewModel.Health.Subscribe(health => { _healthText.SetText(health); });
    }

    private void InstantiateHangman()
    {
        if (_hangmanIndex >= 9)
            _hangmanIndex = 0;

        Instantiate(_hangmanPrefabs[_hangmanIndex], _hangmanTransform);
        _hangmanIndex++;
    }

    private void PlaySequence()
    {
        if (_sequence == null)
        {
            _sequence = DOTween.Sequence();
            _sequence.Insert(0f, _healthText.transform.DOShakePosition(1f, 10f, 50, 30, true));
            _sequence.Insert(0f, _healthText.DOColor(Color.red, 0.2f));
            _sequence.Insert(1f, _healthText.DOColor(Color.white, 0.2f));
            _sequence.Insert(0f, _incorrectLettersText.transform.DOShakePosition(1f, 10f, 50, 30, true));
            _sequence.SetAutoKill(false);
        }
        else
        {
            _sequence.Restart();
        }
    }
}