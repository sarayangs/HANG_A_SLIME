using System;
using System.Threading.Tasks;
using UnityEngine;

public class ChangeSceneUseCase : ISceneHandler
{
    private readonly ISceneHandlerService _sceneHandlerService;
    private readonly IAnalyticsService _analyticsService;
    private readonly IAccessUserData _userRepository;

    public ChangeSceneUseCase(ISceneHandlerService sceneHandlerService, IAnalyticsService analyticsService, IAccessUserData userRepository)
    {
        _sceneHandlerService = sceneHandlerService;
        _analyticsService = analyticsService;
        _userRepository = userRepository;
    }
    public async Task ChangeSceneTo(string scene)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        await _sceneHandlerService.LoadScene(scene);
    }

    public async Task PlayScene()
    {
        _analyticsService.StartLevelEvent(_userRepository.GetLocalUser().CorrectWords + 1);
        await _sceneHandlerService.LoadScene("Play");
    }

    public async Task RetryPlay()
    {
        _analyticsService.StartLevelEvent(_userRepository.GetLocalUser().CorrectWords + 1);
        await _sceneHandlerService.LoadScene("Play");
    }
}