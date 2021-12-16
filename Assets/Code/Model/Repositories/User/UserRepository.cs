using System.Collections.Generic;
using UnityEngine;

public class AccessUserData : IAccessUserData
{
    private UserEntity _userEntity;
    private List<UserEntity> _users;

    public AccessUserData()
    {
        _users = new List<UserEntity>();
    }

    public UserEntity GetLocalUser()
    {
        return _userEntity;
    }
    public void SetLocalUser(UserEntity userEntity)
    {
        _userEntity = userEntity;
    }

  
}