using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SignInUserUseCase : ISignInUser
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IAccessUserData _accessUserData;
    private readonly IRegisteredUsersRepository _registeredUsersRepository;

    public SignInUserUseCase(IAuthenticationService authService, IEventDispatcherService eventDispatcherService
    , IAccessUserData accessUserData, IRegisteredUsersRepository registeredUsersRepository)
    {
        _authenticationService = authService;
        _eventDispatcherService = eventDispatcherService;
        _accessUserData = accessUserData;
        _registeredUsersRepository = registeredUsersRepository;
    }
    
    public async Task SignIn(KeyValuePair<string, string> emailPassword)
    {
        var user = await _authenticationService.SignIn(emailPassword);
        Debug.Log(user.UserId+  user.Name + user.Email +user.Password);
        
        _eventDispatcherService.Dispatch<bool>(false);
    }
}