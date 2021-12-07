using UnityEngine;

public class HomePresenter
{
        private readonly HomeViewModel _viewModel;

        public HomePresenter(HomeViewModel viewModel)
        { 
                _viewModel = viewModel;

                var eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcherService>();
                eventDispatcher.Subscribe<User>(ChangeName);
        }

        private void ChangeName(User data)
        {
                _viewModel.Name.Value = data.Name;
        }
}