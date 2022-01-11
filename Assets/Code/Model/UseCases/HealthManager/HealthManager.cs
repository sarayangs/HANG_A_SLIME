public class HealthManager : IHealthManager
{
    private readonly IAccessUserData _userRepository;
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IUserStatsManager _userStatsManagerUseCase;

    public HealthManager(IAccessUserData userRepository, IEventDispatcherService eventDispatcherService, IUserStatsManager userStatsManagerUseCase)
    {
        _userRepository = userRepository;
        _eventDispatcherService = eventDispatcherService;
        _userStatsManagerUseCase = userStatsManagerUseCase;
    }

    public void InitHealth()
    {
        var user = _userRepository.GetLocalUser();
        user.Health = 9;
        SetNewUser(user);
    }

    public void AddHealth()
    {
        var user = _userRepository.GetLocalUser();
        user.Health++;
        SetNewUser(user);
    }

    public void SubtractHealth()
    {
        var user = _userRepository.GetLocalUser();
        user.Health--;
        
        if (user.Health <= 0)
        {
            _userStatsManagerUseCase.ManageUserStats(false);
        }
        
        SetNewUser(user);
    }

    private void SetNewUser(UserEntity user)
    {
        _userRepository.SetLocalUser(user);
        _eventDispatcherService.Dispatch<UserEntity>(user);
    }

}