using UnityEngine;

public class UpdateUserDataUseCase : IUpdateUserData
{
    public void UpdateName(string newName)
    {
        var authService = ServiceLocator.Instance.GetService<FirebaseAuthService>();
        string userId = authService.GetUserId();
        
        var firestoreService = ServiceLocator.Instance.GetService<FirebaseFirestoreService>();
        var newUser = new User(userId, newName);
        firestoreService.AddToDatabase(newUser);
        
        var eventDispatcherService = ServiceLocator.Instance.GetService<IEventDispatcherService>();
        eventDispatcherService.Dispatch<User>(newUser);
    }
}