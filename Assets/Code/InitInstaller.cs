﻿using System;
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
        
        var sceneHandler = new UnitySceneHandler();
        var accessUserData = new AccessUserData();
        var registeredUsersRepository = new RegisteredUsersRepository();
        
        var eventDispatcher = new EventDispatcherService();


        //FIREBASE
        ServiceLocator.Instance.RegisterService<FirebaseAuthService>(firebaseAuth);
        ServiceLocator.Instance.RegisterService<FirebaseFirestoreService>(firebaseFirestore);
        ServiceLocator.Instance.RegisterService<FirebaseDatabaseService>(firebaseDatabase);
        ServiceLocator.Instance.RegisterService<FirebaseMessagingService>(firebaseMessaging);
        
        ServiceLocator.Instance.RegisterService<UnitySceneHandler>(sceneHandler);
        ServiceLocator.Instance.RegisterService<AccessUserData>(accessUserData);
        ServiceLocator.Instance.RegisterService<RegisteredUsersRepository>(registeredUsersRepository);
        
        ServiceLocator.Instance.RegisterService<IEventDispatcherService>(eventDispatcher);

        //firebaseMessaging.Init();
        
        var authenticateUseCase = new AuthenticateUseCase(firebaseAuth);
        var changeSceneUseCase = new ChangeSceneUseCase(sceneHandler);
        var initNewUserUseCase = new InitNewUserUseCase(firebaseFirestore, firebaseAuth, accessUserData);
        var loadUserDataUseCase = new LoadUserDataUseCase(initNewUserUseCase, accessUserData, registeredUsersRepository, firebaseFirestore, firebaseAuth);
        
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