using UniRx;

public class ButtonsController
{
    public readonly ButtonsViewModel _buttonViewModel;
    private readonly IRankingManager _rankingManagerUseCase;

    public ButtonsController(ButtonsViewModel buttonsViewModel, HomeViewModel homeViewModel, ScoreViewModel scoreViewModel, SettingsViewModel settingsViewModel, 
        IRankingManager rankingManagerUseCase)
    {
        _buttonViewModel = buttonsViewModel;
        _rankingManagerUseCase = rankingManagerUseCase;

        _buttonViewModel.HomeButtonPressed
            .Subscribe(
            (_) =>
            {
                homeViewModel.IsVisible.Value = true;
                scoreViewModel.IsVisible.Value = false;
                settingsViewModel.IsVisible.Value = false;

                _buttonViewModel.HomeIsPressed.Value = true;
                _buttonViewModel.ScoreIsPressed.Value = false;
                _buttonViewModel.SettingsIsPressed.Value = false;

            });

        _buttonViewModel.ScoreButtonPressed
            .Subscribe(
            (_) =>
            {
                homeViewModel.IsVisible.Value = false;
                scoreViewModel.IsVisible.Value = true;
                settingsViewModel.IsVisible.Value = false;
                
                _buttonViewModel.HomeIsPressed.Value = false;
                _buttonViewModel.ScoreIsPressed.Value = true;
                _buttonViewModel.SettingsIsPressed.Value = false;
                
                _rankingManagerUseCase.GetAllData();

            });

        _buttonViewModel.SettingsButtonPressed
            .Subscribe(
            (_) =>
            {
                homeViewModel.IsVisible.Value = false;
                scoreViewModel.IsVisible.Value = false;
                settingsViewModel.IsVisible.Value = true;
                
                _buttonViewModel.HomeIsPressed.Value = false;
                _buttonViewModel.ScoreIsPressed.Value = false;
                _buttonViewModel.SettingsIsPressed.Value = true;
            });
    }
}