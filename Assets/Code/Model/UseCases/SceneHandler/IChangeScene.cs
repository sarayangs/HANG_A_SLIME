using System.Threading.Tasks;

public interface ISceneHandler
{
   Task ChangeSceneTo(string name);
   Task PlayScene();
}