using UniRx;

public class PauseController : Controller
{
    private readonly PauseViewModel _viewModel;
    private readonly ITimeManager _timeManagerUseCase;
    
    public PauseController(PauseViewModel viewModel, ITimeManager timeManagerUseCase)
    {
        _viewModel = viewModel;
        _timeManagerUseCase = timeManagerUseCase;

        _viewModel.OnResumeButtonPressed.Subscribe((_) =>
        {
            _timeManagerUseCase.ResumeTimer();
        }).AddTo(_disposables);
    }
}