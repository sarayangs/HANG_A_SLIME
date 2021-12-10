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
    /*public void CheckExistingUser()
    {
        var firebaseAuthService = ServiceLocator.Instance.GetService<FirebaseAuthService>();
        var firebaseFirestoreService = ServiceLocator.Instance.GetService<FirebaseFirestoreService>();
        firebaseFirestoreService.CheckExistingUser(firebaseAuthService.GetUserId());
    }*/

    /*public async Task LoginNewUser()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
        
        var firebaseAuthService = ServiceLocator.Instance.GetService<FirebaseAuthService>();
        var firebaseFirestoreService = ServiceLocator.Instance.GetService<FirebaseFirestoreService>();
        var firebaseDatabaseService = ServiceLocator.Instance.GetService<FirebaseDatabaseService>();

        firebaseAuthService.Login();
        
        string userId = firebaseAuthService.GetUserId();
        Debug.Log($"New User: {userId}");

        User user = new User(userId, "Sara");
            
        firebaseFirestoreService.AddToDatabase(user);
        firebaseDatabaseService.AddData(user.Score, user.Name);
        
        SceneManager.LoadScene("Menu"); //TEMP!!!!!!
    }*/

   /* public async Task LoginExistingUser()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));

        Debug.Log("Ya existe");
        
        SceneManager.LoadScene("Menu"); //TEMP!!!!!!

    }*/

}