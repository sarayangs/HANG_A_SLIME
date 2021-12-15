using System.Collections.Generic;

public interface IRegisteredUsersRepository
{
    void AddUserToRepository(RegisteredUser userEntity);
    void UpdateUser(RegisteredUser userEntity);
    IReadOnlyList<RegisteredUser> GetAll();
}