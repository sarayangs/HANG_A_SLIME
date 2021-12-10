using UnityEngine;

public class HomePresenter
{
        private readonly HomeViewModel _viewModel;

        public HomePresenter(HomeViewModel viewModel)
        { 
                _viewModel = viewModel;

                var eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcherService>();
                eventDispatcher.Subscribe<string>(ChangeName);
        }

        private void ChangeName(string name)
        {
                _viewModel.Name.Value = name;
        }
}