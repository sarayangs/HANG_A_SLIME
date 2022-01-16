using System;
using System.Collections.Generic;
using UnityEngine;

public class InitInstaller : MonoBehaviour
{
    private readonly List<IDisposable> _disposables = new List<IDisposable>();
    private LoadInitDataUseCase _loadInitDataUseCase;

    private void Awake()
    {
        var firebaseAuth = new FirebaseAuthService();
        var firebaseFirestore = new FirebaseFirestoreService();
        var firebaseDatabase = new FirebaseDatabaseService();
        var firebaseMessaging = new FirebaseMessagingService();
        var firebaseRealtime = new FirebaseDatabaseService();
        var firebaseAnalytics = new FirebaseAnalyticsService();
        
        var sceneHandler = new UnitySceneHandler();
        var accessUserData = new AccessUserData();
        var loggedUsersRepository = new LoggedUsersRepository();
        
        var eventDispatcher = new EventDispatcherService();


        //FIREBASE
        ServiceLocator.Instance.RegisterService<FirebaseAuthService>(firebaseAuth);
        ServiceLocator.Instance.RegisterService<FirebaseFirestoreService>(firebaseFirestore);
        ServiceLocator.Instance.RegisterService<FirebaseDatabaseService>(firebaseDatabase);
        ServiceLocator.Instance.RegisterService<FirebaseMessagingService>(firebaseMessaging);
        ServiceLocator.Instance.RegisterService<FirebaseAnalyticsService>(firebaseAnalytics);
        
        
        ServiceLocator.Instance.RegisterService<UnitySceneHandler>(sceneHandler);
        ServiceLocator.Instance.RegisterService<AccessUserData>(accessUserData);
        ServiceLocator.Instance.RegisterService<LoggedUsersRepository>(loggedUsersRepository);
        
        ServiceLocator.Instance.RegisterService<IEventDispatcherService>(eventDispatcher);

        //firebaseMessaging.Init();
        
        var authenticateUseCase = new AuthenticateUseCase(firebaseAuth);
        var changeSceneUseCase = new ChangeSceneUseCase(sceneHandler, firebaseAnalytics, accessUserData);
        var initNewUserUseCase = new InitNewUserUseCase(firebaseFirestore, firebaseRealtime, firebaseAuth, accessUserData);
        var loadUserDataUseCase = new LoadUserDataUseCase(initNewUserUseCase, accessUserData, loggedUsersRepository, firebaseFirestore, firebaseAuth);
        
        //USECASE
        _loadInitDataUseCase = new LoadInitDataUseCase(changeSceneUseCase, authenticateUseCase, loadUserDataUseCase);
    }

    private void Start()
    {
        _loadInitDataUseCase.Init();
    }
    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}