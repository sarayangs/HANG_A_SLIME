using System;

[Serializable]
public class RegisteredUser: IUserData
{ 
        public string UserId;
        public string Name;
        public string Email;
        public string Password;

        public RegisteredUser(string userid, string name, string email,  string password)
        {
            UserId = userid;
            Name = name;
            Email = email;
            Password = password;
        }
}