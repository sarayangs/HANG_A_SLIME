using System;
using System.Collections.Generic;
using System.Text;
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
            var registeredUserDecrypted = new RegisteredUser(userDto.UserId, userDto.Name, Decrypt(userDto.Email),
                Decrypt(userDto.Password));
            
            _users.Add(registeredUserDecrypted);
        }
        return _users;
    }
    
    private void SaveUsersOnPlayerPrefs()
    {
        var registeredUsers = new List<RegisteredUser>(_users.Count);
        
        foreach (var entity in _users)
        {
            var encryptedUser = new RegisteredUser(entity.UserId, entity.Name, Encrypt(entity.Email),
                Encrypt(entity.Password));
            
            registeredUsers.Add(encryptedUser);
        }

        var json = JsonUtility.ToJson(new UsersDtos(registeredUsers));
        
        PlayerPrefs.SetString(_userKey, json);
        PlayerPrefs.Save();
    }

    private string Encrypt(string info)
    {
        byte[] bytes = ASCIIEncoding.ASCII.GetBytes(info);  
        string encrypted = Convert.ToBase64String(bytes);  
        return encrypted;
    }

    private string Decrypt(string info)
    {
        byte[] bytes;  
        string decrypted;  
        
        try  
        {  
            bytes = Convert.FromBase64String(info);  
            decrypted = ASCIIEncoding.ASCII.GetString(bytes);  
        }  
        catch (FormatException error)   
        {  
            Debug.LogError(error);
            decrypted = "";  
        }  
        
        return decrypted;  
    }

}