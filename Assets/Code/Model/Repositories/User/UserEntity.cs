using System;

[Serializable]
public class UserEntity : IUserData
{
    public readonly string UserId;
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public UserEntity(string userid, string name)
    {
        UserId = userid;
        Name = name;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}