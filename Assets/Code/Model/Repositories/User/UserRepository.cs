﻿public class UserRepository : IAccessUserData
{
    private UserEntity _userEntity;

    public UserEntity GetLocalUser()
    {
        return _userEntity;
    }
    public void SetLocalUser(UserEntity userEntity)
    {
        _userEntity = userEntity;
    }
}