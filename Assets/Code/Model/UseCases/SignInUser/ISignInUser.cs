using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISignInUser
{
    Task SignIn(KeyValuePair<string, string> emailPassword);        
}