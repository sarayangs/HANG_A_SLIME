public interface IAccessUserData
{
    UserEntity GetLocalUser();
    void SetLocalUser(UserEntity userEntity);
}