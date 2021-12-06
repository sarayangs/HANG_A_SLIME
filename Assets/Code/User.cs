using Firebase.Firestore;

[FirestoreData]
public class User
{
    [FirestoreDocumentId]
    public string Id { get; set; }
    [FirestoreProperty]
    public string Name { get; set; }

    public User()
    {
    }

    public User(string id, string name)
    {
        Id = id;
        Name = name;
    }
}