using UnityEngine;
using UniRx;
using DG.Tweening;

public class SettingsView : MonoBehaviour
{
    private SettingsViewModel _viewModel;
    public void Setup(SettingsViewModel settingsViewModel)
    {
        _viewModel = settingsViewModel;
        
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
    }
}