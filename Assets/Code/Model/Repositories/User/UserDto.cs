using Firebase.Firestore;

[FirestoreData]
public class UserDto : IUserData
{
    [FirestoreDocumentId]
    public string Id { get; set; }
    [FirestoreProperty]
    public string Name { get; set; }
    
    public string Email { get; set; }
    public string Password { get; set; }

    public int Score;

    public UserDto()
    {
    }

    public UserDto(string id, string name)
    {
        Id = id;
        Name = name;
        Score = 0;
    }

    public void UpdateScore(int score)
    {
        Score = score;
    }
}