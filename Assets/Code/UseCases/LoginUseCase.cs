using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginUseCase : ILogin
{
    public async Task Login()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
        
        var firebaseAuthService = ServiceLocator.Instance.GetService<FirebaseAuthService>();
        
        if (firebaseAuthService.CheckExistingUser())
        {
            var userId = firebaseAuthService.GetUserId();
            Debug.Log($"Existing User: {userId}");
            
            /*var firebaseFirestoreService = ServiceLocator.Instance.GetService<FirebaseFirestoreService>();
            firebaseFirestoreService.GetData(userId);*/
            
        }
        else
        {
            firebaseAuthService.LoginNewUser();
            string userId = firebaseAuthService.GetUserId();
            Debug.Log($"New User: {userId}");
            
            var firebaseFirestoreService = ServiceLocator.Instance.GetService<FirebaseFirestoreService>();
            firebaseFirestoreService.AddToDatabase(new User(userId, "Sara"));
        }
        
        SceneManager.LoadScene("Menu"); //TEMP!!!!!!
        
    }
}