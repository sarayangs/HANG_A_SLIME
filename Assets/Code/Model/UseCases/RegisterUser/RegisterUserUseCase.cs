using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RegisterUserUseCase :IRegisterUser
{
    private readonly IAuthenticationService _authenticationService;
    private List<UserEntity> _users;

    public RegisterUserUseCase(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    public async Task Register(KeyValuePair<string, string> emailPassword)
    {
        var user = await _authenticationService.RegisterUser(emailPassword);
    }
    
}