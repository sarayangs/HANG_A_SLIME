using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;

public class FirebaseAuthService : IAuthenticationService
{
    private readonly Firebase.Auth.FirebaseAuth _auth;
    public string UserId { get; set; }
    public FirebaseAuthService()
    {
        _auth =  Firebase.Auth.FirebaseAuth.DefaultInstance;
    }
    public async Task<string> Login()
    {
        var user = await _auth.SignInAnonymouslyAsync();
        
        if (user != null)
        {
            UserId = user.UserId;
            return UserId;
        }
        throw new Exception("SignInAnonymouslyAsync error.");
    }

    /*public void CreateEmailAndPassword(KeyValuePair<string, string> info)
    {
        _auth.CreateUserWithEmailAndPasswordAsync(info.Key, info.Value).ContinueWithOnMainThread(task => {
            if (task.IsCanceled) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            
            
        });
    }*/
}
