using System;

[Serializable]
public class UserEntity : IUserData
{
    public readonly string UserId;
    public string Name { get; set; }
    public bool Notifications { get; set; }
    public bool Audio { get; set; }
    
    public int Score { get; set; }

    public int Health { get; set; }
    
    public int CorrectWords { get; set; }
    
    public float Time { get; set; }

    public UserEntity(string userid, string name, bool notifications, bool audio, int score)
    {
        UserId = userid;
        Name = name;
        Notifications = notifications;
        Audio = audio;
        Score = score;
        CorrectWords = 0;
        Health = 9;
        Time = 0;
    }
}