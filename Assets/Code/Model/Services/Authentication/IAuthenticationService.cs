using System.Threading.Tasks;

public interface IAuthenticationService
{
    public string UserId { get; }
    Task<string> Login();
}