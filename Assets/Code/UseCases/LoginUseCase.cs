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
            
           User user = new User(userId, "Sara");
            
            var firebaseDatabaseService = ServiceLocator.Instance.GetService<FirebaseDatabaseService>();
            firebaseDatabaseService.AddData(user.Score, user.Name);
        }
        else
        {
            firebaseAuthService.LoginNewUser();
            string userId = firebaseAuthService.GetUserId();
            Debug.Log($"New User: {userId}");

            User user = new User(userId, "Sara");
            
            var firebaseFirestoreService = ServiceLocator.Instance.GetService<FirebaseFirestoreService>();
            firebaseFirestoreService.AddToDatabase(user);
           
        }
        SceneManager.LoadScene("Menu"); //TEMP!!!!!!
    }
}