using System;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseAuthService
{
    private readonly Firebase.Auth.FirebaseAuth _auth;

    public FirebaseAuthService()
    {
        _auth =  Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public bool CheckExistingUser()
    {
        return _auth.CurrentUser != null;
    }

    public string GetUserId()
    {
        return _auth.CurrentUser.UserId;
    }

    public void Login()
    {
       _auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                throw new Exception("SignInAnonymouslyAsync was canceled.");
            }
            if (task.IsFaulted) {
                throw new Exception("SignInAnonymouslyAsync encountered an error: " + task.Exception);
            }
       });
    }

    public void CreateEmailAndPassword(KeyValuePair<string, string> info)
    {
        _auth.CreateUserWithEmailAndPasswordAsync(info.Key, info.Value).ContinueWith(task => {
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
    }
}
