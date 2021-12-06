using System;
using UnityEngine;

public class InitInstaller : MonoBehaviour
{
    [SerializeField] private InitView _initView;
    private void Awake()
    {
        var firebaseAuth = new FirebaseAuthService();
        ServiceLocator.Instance.RegisterService<FirebaseAuthService>(firebaseAuth);
        
        var firebaseFirestore = new FirebaseFirestoreService();
        ServiceLocator.Instance.RegisterService<FirebaseFirestoreService>(firebaseFirestore);
        
        
        var initViewModel = new InitViewModel();
        _initView.Setup(initViewModel);
        
        var eventDispatcher = new EventDispatcherService();
        
        var loginUseCase = new LoginUseCase(eventDispatcher);

        new InitController(initViewModel, loginUseCase);
    }
}