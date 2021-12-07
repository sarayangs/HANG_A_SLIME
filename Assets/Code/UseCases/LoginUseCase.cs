using Firebase.Firestore;
using UnityEngine;

public class LoginUseCase : ILogin
{
    private readonly IEventDispatcherService _eventDispatcher;
    
    public LoginUseCase(IEventDispatcherService eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;
    }

    public void Login()
    {
        var firebaseAuthService = ServiceLocator.Instance.GetService<FirebaseAuthService>();
        
        if (firebaseAuthService.CheckExistingUser())
        {
            var userId = firebaseAuthService.GetUserId();
            Debug.Log($"Existing User: {userId}");
            
            var firebaseFirestoreService = ServiceLocator.Instance.GetService<FirebaseFirestoreService>();
            firebaseFirestoreService.GetData(userId);
            
        }
        else
        {
            Debug.Log("New User");
            string userId = firebaseAuthService.GetNewUserId();
            
            var firebaseFirestoreService = ServiceLocator.Instance.GetService<FirebaseFirestoreService>();
            firebaseFirestoreService.AddToDatabase(new User(userId, "Sara"));
        }
        
        
        


    }
}