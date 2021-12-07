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
        
        var eventDispatcher = new EventDispatcherService();
        ServiceLocator.Instance.RegisterService<IEventDispatcherService>(eventDispatcher);
        
        var initViewModel = new InitViewModel();
        _initView.Setup(initViewModel);
        
        
        var loginUseCase = new LoginUseCase();

        new InitController(initViewModel, loginUseCase);
    }
}