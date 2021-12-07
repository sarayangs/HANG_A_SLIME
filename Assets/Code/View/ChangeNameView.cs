using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ChangeNameView : MonoBehaviour
{
    [SerializeField] private Button _saveButton;
    [SerializeField] private TMP_InputField _newName;

    private ChangeNameViewModel _viewModel;

    public void Setup(ChangeNameViewModel viewModel)
    {
        _viewModel = viewModel;
        _viewModel.IsVisible.Subscribe(isVisible =>
        {
            gameObject.SetActive(isVisible);
        });
        
        _saveButton.onClick.AddListener(() =>
        {
            _viewModel.SaveButtonPressed.Execute(_newName.text);
        });
    }
}
