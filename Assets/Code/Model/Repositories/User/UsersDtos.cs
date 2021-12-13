
using System;
using System.Collections.Generic;

[Serializable]
public class UsersDtos
{
    public List<UserDto> RegisteredUsers;

    public UsersDtos(List<UserDto> registeredUsers)
    {
        RegisteredUsers = registeredUsers;
    }  
}