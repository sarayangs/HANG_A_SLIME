public class HealthManager : IHealthManager
{
    private readonly IAccessUserData _userRepository;
    private readonly IEventDispatcherService _eventDispatcherService;

    public HealthManager(IAccessUserData userRepository, IEventDispatcherService eventDispatcherService)
    {
        _userRepository = userRepository;
        _eventDispatcherService = eventDispatcherService;
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
            _eventDispatcherService.Dispatch<Answer>(new Answer(false));
        }
        
        SetNewUser(user);
    }

    private void SetNewUser(UserEntity user)
    {
        _userRepository.SetLocalUser(user);
        _eventDispatcherService.Dispatch<UserEntity>(user);
    }

}