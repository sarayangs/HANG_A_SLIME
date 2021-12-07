using System;

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

    public void LoginNewUser()
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
}
