
public class LogoutUserUseCase : ILogoutUser
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IEventDispatcherService _eventDispatcherService;

    public LogoutUserUseCase(IAuthenticationService authenticationService, IEventDispatcherService eventDispatcherService)
    {
        _authenticationService = authenticationService;
        _eventDispatcherService = eventDispatcherService;

    }
    public void Logout()
    {
        _authenticationService.Logout();
        _eventDispatcherService.Dispatch<bool>(false);
    }
}