using System;
using System.Threading.Tasks;

public class ChangeSceneUseCase : ISceneHandler
{
    private readonly ISceneHandlerService _sceneHandlerService;
    private readonly IAnalyticsService _analyticsService;

    private int _level = 1;
    
    public ChangeSceneUseCase(ISceneHandlerService sceneHandlerService, IAnalyticsService analyticsService)
    {
        _sceneHandlerService = sceneHandlerService;
        _analyticsService = analyticsService;
    }
    public async Task ChangeSceneTo(string scene)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        await _sceneHandlerService.LoadScene(scene);
    }

    public async Task PlayScene()
    {
        _analyticsService.StartLevelEvent(_level);
        _level++;
        
        await _sceneHandlerService.LoadScene("Play");
    }

    public async Task RetryPlay()
    {
        _level = 1;
        _analyticsService.StartLevelEvent(_level);
        _level++;
        
        await _sceneHandlerService.LoadScene("Play");
    }
}