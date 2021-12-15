using System.Collections.Generic;
using UnityEngine;

public class RegisteredUsersRepository : IRegisteredUsersRepository
{
    private readonly string _userKey = "UserKey";
    
    private List<RegisteredUser> _users;
    
    public RegisteredUsersRepository()
    {
        _users = new List<RegisteredUser>();
    }

    public void AddUserToRepository(RegisteredUser userEntity)
    {
        _users.Add(userEntity);
        SaveUsersOnPlayerPrefs();
    }

    public void UpdateUser(RegisteredUser userEntity)
    {
        foreach (var user in _users)
        {
            if (userEntity.UserId == user.UserId)
            {
                user.Name = userEntity.Name;
                SaveUsersOnPlayerPrefs();
                return;
            }
        }        
    }

    public IReadOnlyList<RegisteredUser> GetAll()
    {
        //PlayerPrefs.DeleteKey(_userKey);
        var defaultValue = new UsersDtos(new List<RegisteredUser>());
        var usersJson = PlayerPrefs.GetString(_userKey, JsonUtility.ToJson(defaultValue));
        var users = JsonUtility.FromJson<UsersDtos>(usersJson);
        
        if (users.RegisteredUsers == null)
            return null;
        
        foreach (var userDto in users.RegisteredUsers)
        {
            Debug.Log(userDto.UserId);
            _users.Add(userDto);
        }
        return _users;
    }
    
    private void SaveUsersOnPlayerPrefs()
    {
        var registeredUsers = new List<RegisteredUser>(_users.Count);
        
        foreach (var entity in _users)
        {
            registeredUsers.Add(entity);
        }

        var json = JsonUtility.ToJson(new UsersDtos(registeredUsers));
        
        PlayerPrefs.SetString(_userKey, json);
        PlayerPrefs.Save();
    }
}