using System;

public class FirebaseAuthService
{
    private readonly Firebase.Auth.FirebaseAuth auth;

    public FirebaseAuthService()
    {
        auth =  Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public bool CheckExistingUser()
    {
        return auth.CurrentUser != null;
    }

    public string GetNewUserId()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                throw new Exception("SignInAnonymouslyAsync was canceled.");
            }
            if (task.IsFaulted) {
                throw new Exception("SignInAnonymouslyAsync encountered an error: " + task.Exception);
            }
        });
        
        return auth.CurrentUser.UserId;
    }
}
