using UniRx;

public class PauseController : Controller
{
    private readonly PauseViewModel _viewModel;
    private readonly ITimeManager _timeManagerUseCase;
    private readonly ISoundHandler _soundUseCase;
    
    public PauseController(PauseViewModel viewModel, ITimeManager timeManagerUseCase,  ISoundHandler soundUseCase)
    {
        _viewModel = viewModel;
        _timeManagerUseCase = timeManagerUseCase;
        _soundUseCase = soundUseCase;

        _viewModel.OnResumeButtonPressed.Subscribe((_) =>
        {
            _timeManagerUseCase.ResumeTimer();
            _soundUseCase.Play("pause");
        }).AddTo(_disposables);
    }
}