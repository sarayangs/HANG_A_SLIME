using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthenticateUseCase : IAuthenticator
{
    private readonly IAuthenticationService _authenticationService;
    
    public AuthenticateUseCase(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    public async Task Authenticate()
    {
        var userId = await _authenticationService.Login();
    }
}