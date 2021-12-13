using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAuthenticationService
{
    public string UserId { get; }
    Task<string> Login();
    Task<UserEntity> RegisterUser(KeyValuePair<string, string> info);
}