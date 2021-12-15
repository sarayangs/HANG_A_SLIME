using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAuthenticationService
{
    public string UserId { get; }
    Task<string> Login();
    Task<RegisteredUser> RegisterUser(KeyValuePair<string, string> info);
    Task<RegisteredUser> SignIn(KeyValuePair<string, string> info);
}