using System.Threading.Tasks;

public interface IDatabaseService
{
    Task<bool> ExistKey(string collection, string key);
    Task Save<T>(T userData, string collection, string key) where T : IUserData;
    Task<T> Load<T>(string collection, string key);
}