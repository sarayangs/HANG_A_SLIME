public class PausePresenter : Presenter
{
    private readonly PauseViewModel _viewModel;
    private readonly IEventDispatcherService _eventDispatcherService;
        
    public PausePresenter(PauseViewModel viewModel, IEventDispatcherService eventDispatcherService)
    {
        _viewModel = viewModel;
        _eventDispatcherService = eventDispatcherService; 
                
        _eventDispatcherService.Subscribe<float>(OnResumePressed);
    }

    public override void Dispose()
    {
        base.Dispose();
        _eventDispatcherService.Unsubscribe<float>(OnResumePressed);
    }

    private void OnResumePressed(float data)
    {
        _viewModel.IsVisible.Value = false; 
    }
}