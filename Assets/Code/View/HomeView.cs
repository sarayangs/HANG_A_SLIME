using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using DG.Tweening;

public class HomeView : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _changeNameButton;
    [SerializeField] private TextMeshProUGUI _name;
    
    private HomeViewModel _viewModel;
    
    public void Setup(HomeViewModel homeViewModel)
    {
        _viewModel = homeViewModel;
        
        _viewModel.IsVisible.Subscribe((isVisible) =>
            {
                if (isVisible)
                {
                    gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(transform.parent.gameObject.GetComponent<RectTransform>().rect.width, 0, 0);
                    gameObject.SetActive(isVisible);
                    gameObject.GetComponent<RectTransform>().DOLocalMoveX(0, 0.5f);
                }
                else
                {
                    gameObject.GetComponent<RectTransform>().DOLocalMoveX(-transform.parent.gameObject.GetComponent<RectTransform>().rect.width, 0.5f).OnComplete(() => { gameObject.SetActive(isVisible); });
                }
            });
        
        _playButton.onClick.AddListener(() =>
        {
            _viewModel.PlayButtonPressed.Execute();
        });
        
        _changeNameButton.onClick.AddListener(() =>
        {
            _viewModel.ChangeNameButtonPressed.Execute();
        });

        _viewModel.Name.Subscribe(name =>
        {
            _name.SetText(name);
        });
    }
}