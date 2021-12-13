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

    public async Task<UserEntity> RegisterUser(KeyValuePair<string, string> info)
    {
        FirebaseUser user = null;
        
        try
        {
            user = await _auth.CreateUserWithEmailAndPasswordAsync(info.Key, info.Value);
        }
        catch (Exception x)
        {
            Debug.LogError(x);

        }
        
        if (user != null) {
            Debug.Log($"user registered: {info.Key}, {info.Value}");
            
            var userEntity = new UserEntity(UserId, string.Empty);
            
            userEntity.Email = info.Key;
            userEntity.Password = info.Value;
            
            return userEntity;
        }
        throw new Exception("CreateUserWithEmailAndPasswordAsync error.");
    }
}
