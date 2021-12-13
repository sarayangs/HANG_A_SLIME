using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RegisterUserUseCase :IRegisterUser
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IAccessUserData _userRepository;
    private readonly IEventDispatcherService _eventDispatcherService;
    
    private List<UserEntity> _users;

    public RegisterUserUseCase(IAuthenticationService authenticationService, IAccessUserData userRepository, IEventDispatcherService eventDispatcherService)
    {
        _authenticationService = authenticationService;
        _eventDispatcherService = eventDispatcherService;
        _userRepository = userRepository;
    }
    
    public async Task Register(KeyValuePair<string, string> emailPassword)
    {
        var user = await _authenticationService.RegisterUser(emailPassword);
        user.Name = _userRepository.GetLocalUser().Name;
        
        _userRepository.SetLocalUserWithMailAndPass(user);
        
        _eventDispatcherService.Dispatch<bool>(false);
        
    }
    
}