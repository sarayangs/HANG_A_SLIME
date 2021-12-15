using System;

[Serializable]
public class UserEntity : IUserData
{
    public readonly string UserId;
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Notifications { get; set; }
    public bool Audio { get; set; }

    public UserEntity(string userid, string name, bool notifications, bool audio)
    {
        UserId = userid;
        Name = name;
        Notifications = notifications;
        Audio = audio;
    }
}