using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneUseCase : IChangeScene
{
    public void ChangeSceneTo(string scene)
    {
        Debug.Log($"Changing scene to {scene}");
        SceneManager.LoadScene(scene);
    }
}