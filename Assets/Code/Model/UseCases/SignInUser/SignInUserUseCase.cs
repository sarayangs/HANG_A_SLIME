using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SignInUserUseCase : ISignInUser
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IAccessUserData _accessUserData;
    private readonly IDatabaseService _databaseService;
    private readonly ILoggedUsersRepository _loggedUsersRepository;

    public SignInUserUseCase(IAuthenticationService authService, IEventDispatcherService eventDispatcherService
    , IAccessUserData accessUserData, ILoggedUsersRepository loggedUsersRepository, IDatabaseService databaseService)
    {
        _authenticationService = authService;
        _eventDispatcherService = eventDispatcherService;
        _accessUserData = accessUserData;
        _loggedUsersRepository = loggedUsersRepository;
        _databaseService = databaseService;
    }
    
    public async Task SignIn(KeyValuePair<string, string> emailPassword)
    {
        var user = await _authenticationService.SignIn(emailPassword);
        
        var newUser = await _databaseService.Load<UserDto>("users", user.UserId);
        var userEntity = new UserEntity(newUser.Id, newUser.Name, newUser.Notifications, newUser.Audio, newUser.Score);
        
        //Debug.Log(user.UserId+  user.Name + user.Email +user.Password);
        
        _accessUserData.SetLocalUser(userEntity);
        _loggedUsersRepository.AddUserToRepository(user);

        _eventDispatcherService.Dispatch<string>(userEntity.Name);
        _eventDispatcherService.Dispatch<bool>(true);
    }
}