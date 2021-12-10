public class ChangeNamePresenter
{
        private readonly ChangeNameViewModel _viewModel;

        public ChangeNamePresenter(ChangeNameViewModel viewModel)
        {
                _viewModel = viewModel;
                
                var eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcherService>();
                eventDispatcher.Subscribe<string>((data) =>
                {
                        _viewModel.IsVisible.Value = false;
                });
        }
}