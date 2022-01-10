using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class ResultPopupView : View
{
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _time;
        [SerializeField] private RectTransform _buttonsParent;

        [SerializeField] private GameObject _homeButtonPrefab;
        [SerializeField] private GameObject _retryButtonPrefab;
        [SerializeField] private GameObject _nextWordButtonPrefab;
        [SerializeField] private Sprite _sadImage;
        [SerializeField] private Sprite _happyImage;
        
        private ResultPopupViewModel _viewModel;

        public void Setup(ResultPopupViewModel resultViewModel)
        {
                _viewModel = resultViewModel;

                _viewModel.Win.Subscribe(hasWon =>
                {
                        if(hasWon)
                        {
                                _image.sprite = _happyImage;
                                
                        }
                }).AddTo(_disposables);
        }
}