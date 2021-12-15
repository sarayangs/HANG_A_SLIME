using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using UnityEngine;

public class FirebaseAuthService : IAuthenticationService
{
    private readonly Firebase.Auth.FirebaseAuth _auth;
    public string UserId { get; set; }
    public FirebaseAuthService()
    {
        _auth =  FirebaseAuth.DefaultInstance;
    }
    public async Task<string> Login()
    {
        var checkDependencies = await Firebase.FirebaseApp.CheckDependenciesAsync();

        if (checkDependencies == DependencyStatus.Available)
        {
            if (_auth.CurrentUser != null)
            {
                UserId = _auth.CurrentUser.UserId;
                return UserId;
            }
            var user = await _auth.SignInAnonymouslyAsync();
        
            if (user != null)
            {
                UserId = user.UserId;
                return UserId;
            }
            throw new Exception("SignInAnonymouslyAsync error.");
        }
 
        throw new Exception("Dependency error");
       
    }

    public async Task<RegisteredUser> RegisterUser(KeyValuePair<string, string> info)
    {
        FirebaseUser user = null;
        
        try
        {
            user = await _auth.CreateUserWithEmailAndPasswordAsync(info.Key, info.Value);
        }
        catch (Exception error)
        {
            Debug.LogError(error);
        }
        
        if (user != null) {
            Debug.Log($"user registered: {info.Key}, {info.Value}");
            
            var userEntity = new RegisteredUser(user.UserId, user.DisplayName, info.Key, info.Value);
            return userEntity;
        }
        throw new Exception("CreateUserWithEmailAndPasswordAsync error.");
    }

    public async Task<RegisteredUser> SignIn(KeyValuePair<string, string> info)
    {

        FirebaseUser user = null;

        try
        {
            user = await _auth.SignInWithEmailAndPasswordAsync(info.Key, info.Value);
        }
        catch (Exception error)
        {
            Debug.Log(error);
        }

        if (user != null)
        {
            var registeredUser = new RegisteredUser(user.UserId, user.DisplayName, user.Email, info.Value);
            return registeredUser;
        }

        return null;
    }
}
