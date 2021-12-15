using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RegisterUserUseCase :IRegisterUser
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRegisteredUsersRepository _registeredUsersRepository;
    private readonly IAccessUserData _accessUserData;
    private readonly IEventDispatcherService _eventDispatcherService;

    public RegisterUserUseCase(IAuthenticationService authenticationService, IAccessUserData accessUserData, IRegisteredUsersRepository registeredUsersRepository, IEventDispatcherService eventDispatcherService)
    {
        _authenticationService = authenticationService;
        _eventDispatcherService = eventDispatcherService;
        _registeredUsersRepository = registeredUsersRepository;
        _accessUserData = accessUserData;
    }
    
    public async Task Register(KeyValuePair<string, string> emailPassword)
    {
        var user = await _authenticationService.RegisterUser(emailPassword);
        user.Name = _accessUserData.GetLocalUser().Name;
        
        _registeredUsersRepository.AddUserToRepository(user);
        
        _eventDispatcherService.Dispatch<bool>(false);
        
    }
}