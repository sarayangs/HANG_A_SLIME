using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class ScoreItemView : MonoBehaviour
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
        });
        _scoreItemViewModel.Name.Subscribe(name =>
        {
            _name.SetText(name);
        });
        _scoreItemViewModel.Score.Subscribe(score =>
        {
            _score.SetText(score);
        });
        _scoreItemViewModel.Time.Subscribe(time =>
        {
            _score.SetText(time + "m");
        });
    }

}