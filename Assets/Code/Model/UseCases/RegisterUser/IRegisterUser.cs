using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRegisterUser
{
   Task Register(KeyValuePair<string, string> emailPassword);
}