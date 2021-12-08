using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class ScoreItemView : View
{
    [SerializeField] private TextMeshProUGUI _position;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _time;

    private ScoreItemViewModel _scoreItemViewModel;

    public void Setup(ScoreItemViewModel scoreItemViewModel)
    {
        _scoreItemViewModel = scoreItemViewModel;

        _scoreItemViewModel.Position.Subscribe(position =>
        {
            _position.SetText(position);
        }).AddTo(_disposables);
        
        _scoreItemViewModel.Name.Subscribe(name =>
        {
            _name.SetText(name);
        }).AddTo(_disposables);
        
        _scoreItemViewModel.Score.Subscribe(score =>
        {
            _score.SetText(score);
        }).AddTo(_disposables);
        
        _scoreItemViewModel.Time.Subscribe(time =>
        {
            _time.SetText(time + "m");  
        }).AddTo(_disposables);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}