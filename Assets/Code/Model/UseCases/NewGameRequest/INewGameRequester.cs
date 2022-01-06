using System.Threading.Tasks;

public interface INewGameRequester
{
    Task StartGame();
    void SetUserData();
}