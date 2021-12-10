using System.Threading.Tasks;

 public interface IAuthenticator
 {
     Task Authenticate();
     /*void CheckExistingUser();
    Task LoginNewUser();
    Task LoginExistingUser();*/
 }