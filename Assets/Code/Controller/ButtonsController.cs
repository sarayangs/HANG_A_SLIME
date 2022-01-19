using UniRx;

public class ButtonsController : Controller
{
    private readonly ButtonsViewModel _buttonViewModel;
    private readonly IRankingManager _rankingManagerUseCase;
    private readonly ISoundHandler _soundUseCase;

    public ButtonsController(ButtonsViewModel buttonsViewModel, HomeViewModel homeViewModel, ScoreViewModel scoreViewModel, SettingsViewModel settingsViewModel, 
        IRankingManager rankingManagerUseCase, ISoundHandler soundUseCase)
    {
        _buttonViewModel = buttonsViewModel;
        _rankingManagerUseCase = rankingManagerUseCase;
        _soundUseCase = soundUseCase;

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
                    
                    _soundUseCase.Play("swipe");

                }).AddTo(_disposables);

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
                
                _soundUseCase.Play("swipe");

                _rankingManagerUseCase.GetAllData();

            }).AddTo(_disposables);

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
                _soundUseCase.Play("swipe");

            }).AddTo(_disposables);
    }
}