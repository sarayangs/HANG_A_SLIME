using System;
using System.Collections.Generic;

[Serializable]
public class UsersDtos
{
    public List<RegisteredUser> RegisteredUsers;

    public UsersDtos(List<RegisteredUser> registeredUsers)
    {
        RegisteredUsers = registeredUsers;
    }  
}