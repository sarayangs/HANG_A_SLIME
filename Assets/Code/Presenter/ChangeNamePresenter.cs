public class ChangeNamePresenter : Presenter
{
        private readonly ChangeNameViewModel _viewModel;
        private readonly IEventDispatcherService _eventDispatcherService;
        
        public ChangeNamePresenter(ChangeNameViewModel viewModel, IEventDispatcherService eventDispatcherService)
        {
                _viewModel = viewModel;
                _eventDispatcherService = eventDispatcherService; 
                
                _eventDispatcherService.Subscribe<string>(OnChangeNamePressed);
        }

        public override void Dispose()
        {
                base.Dispose();
                _eventDispatcherService.Unsubscribe<string>(OnChangeNamePressed);
        }

        private void OnChangeNamePressed(string data)
        {
                _viewModel.IsVisible.Value = false; 
        }
}