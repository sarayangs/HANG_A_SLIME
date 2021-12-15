using System.Collections.Generic;

public interface IAccessUserData
{
    UserEntity GetLocalUser();
    void SetLocalUser(UserEntity userEntity);

}