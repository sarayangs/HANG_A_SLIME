using System;
using System.Threading.Tasks;

public class ChangeSceneUseCase : ISceneHandler
{
    private readonly ISceneHandlerService _sceneHandlerService;
    public ChangeSceneUseCase(ISceneHandlerService sceneHandlerService)
    {
        _sceneHandlerService = sceneHandlerService;
    }
    public async Task ChangeSceneTo(string scene)
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
        await _sceneHandlerService.LoadScene(scene);
    }
}