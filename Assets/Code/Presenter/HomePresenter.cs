using UnityEngine;

public class HomePresenter : Presenter
{
        private readonly HomeViewModel _viewModel;
        private readonly IEventDispatcherService _eventDispatcherService;

        public HomePresenter(HomeViewModel viewModel, IEventDispatcherService eventDispatcherService)
        { 
                _viewModel = viewModel;
                _eventDispatcherService = eventDispatcherService;
                
                _eventDispatcherService.Subscribe<string>(ChangeName);
        }
        public override void Dispose()
        {
                base.Dispose();
                _eventDispatcherService.Unsubscribe<string>(ChangeName);
        }
        private void ChangeName(string name)
        {
                _viewModel.Name.Value = name;
        }
}