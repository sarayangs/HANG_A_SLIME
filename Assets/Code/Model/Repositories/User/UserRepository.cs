using System.Collections.Generic;
using UnityEngine;

public class UserRepository : IAccessUserData
{
    private readonly string _userKey = "UserKey";
    
    private UserEntity _userEntity;
    private List<UserEntity> _users;

    public UserRepository()
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

    public void SetLocalUserWithMailAndPass(UserEntity userEntity)
    {
        _users.Add(userEntity);
        SaveUsersOnPlayerPrefs();
        
        SetLocalUser(userEntity);
    }

    public IReadOnlyList<UserEntity> GetAll()
    {
        var defaultValue = new UsersDtos(new List<UserDto>());
        var usersJson = PlayerPrefs.GetString(_userKey, JsonUtility.ToJson(defaultValue));
        var users = JsonUtility.FromJson<UsersDtos>(usersJson);

        if (users.RegisteredUsers == null)
            return null;
        
        foreach (var userDto in users.RegisteredUsers)
        {
            var userEntity = new UserEntity(userDto.Id, userDto.Name);
            _users.Add(userEntity);
        }

        return _users;
    }

    private void SaveUsersOnPlayerPrefs()
    {
        var registeredUsers = new List<UserDto>(_users.Count);
        foreach (var entity in registeredUsers)
        {
            registeredUsers.Add(new UserDto(entity.Id, entity.Name));
        }

        var json = JsonUtility.ToJson(new UsersDtos(registeredUsers));
        
        // Guardar users en PlayerPrefs
        PlayerPrefs.SetString(_userKey, json);
        PlayerPrefs.Save();
    }
}