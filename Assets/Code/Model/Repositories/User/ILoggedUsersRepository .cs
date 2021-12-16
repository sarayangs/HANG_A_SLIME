using System.Collections.Generic;

public interface ILoggedUsersRepository
{
    void AddUserToRepository(RegisteredUser userEntity);
    void UpdateUser(RegisteredUser userEntity);
    IReadOnlyList<RegisteredUser> GetAll();
}