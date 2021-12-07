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

    public string GetUserId()
    {
        return auth.CurrentUser.UserId;
    }

    public string GetNewUserId()
    {
        string userId = string.Empty;
        
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                throw new Exception("SignInAnonymouslyAsync was canceled.");
            }
            if (task.IsFaulted) {
                throw new Exception("SignInAnonymouslyAsync encountered an error: " + task.Exception);
            }

            userId = auth.CurrentUser.UserId;

        });
        
        return userId;
    }
}
