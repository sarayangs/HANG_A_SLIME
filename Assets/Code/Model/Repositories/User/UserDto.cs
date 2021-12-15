using Firebase.Firestore;

[FirestoreData]
public class UserDto : IUserData
{
    [FirestoreDocumentId]
    public string Id { get; set; }
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty] 
    public bool Notifications { get; set; }
    [FirestoreProperty] 
    public bool Audio { get; set; }
    
    public int Score { get; set; }
    
    public UserDto()
    {
    }

    public UserDto(string id, string name, bool notifications, bool audio)
    {
        Id = id;
        Name = name;
        Notifications = notifications;
        Audio = audio;
        Score = 0;
    }
}