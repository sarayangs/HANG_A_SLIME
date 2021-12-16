using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RegisterUserUseCase :IRegisterUser
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IAccessUserData _accessUserData;
    private readonly ILoggedUsersRepository _loggedUsersRepository;
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IDatabaseService _databaseService;

    public RegisterUserUseCase(IAuthenticationService authenticationService, IAccessUserData accessUserData, IEventDispatcherService eventDispatcherService,
        IDatabaseService databaseService, ILoggedUsersRepository loggedUsersRepository)
    {
        _authenticationService = authenticationService;
        _eventDispatcherService = eventDispatcherService;
        _accessUserData = accessUserData;
        _databaseService = databaseService;
        _loggedUsersRepository = loggedUsersRepository;
    }
    
    public async Task Register(KeyValuePair<string, string> emailPassword)
    {
        var user = await _authenticationService.RegisterUser(emailPassword);
        
        var currentUser = _accessUserData.GetLocalUser();
        var userEntity = new UserEntity(user.UserId, currentUser.Name, currentUser.Notifications, currentUser.Audio);        
        var databaseUser = new UserDto(user.UserId, currentUser.Name, currentUser.Notifications, currentUser.Audio);
        
        await _databaseService.Save(databaseUser, "users", user.UserId);
        
        _accessUserData.SetLocalUser(userEntity);
        _loggedUsersRepository.AddUserToRepository(user);
        
        _eventDispatcherService.Dispatch<bool>(true);
        
    }
}