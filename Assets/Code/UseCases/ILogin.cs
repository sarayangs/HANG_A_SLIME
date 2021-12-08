using System.Threading.Tasks;

 public interface ILogin
 {
     void CheckExistingUser();
    Task LoginNewUser();
    Task LoginExistingUser();
 }