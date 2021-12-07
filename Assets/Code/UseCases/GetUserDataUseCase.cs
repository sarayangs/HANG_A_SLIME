using UnityEngine;

public class GetUserDataUseCase : IGetUserData
{
    public async void GetUserData()
    {
        var authService = ServiceLocator.Instance.GetService<FirebaseAuthService>();
        string userId = authService.GetUserId();
        
        var firestoreService = ServiceLocator.Instance.GetService<FirebaseFirestoreService>();
        firestoreService.GetData(userId);
    }
}