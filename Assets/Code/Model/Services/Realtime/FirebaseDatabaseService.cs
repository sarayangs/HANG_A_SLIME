using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseDatabaseService : IRealtimeDatabase
{
    private DatabaseReference databaseReference;

    public FirebaseDatabaseService()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void AddData(ScoreEntry entry)
    {
        var jsonValue = JsonUtility.ToJson(entry);

        databaseReference
            .Child("scores")
            .Child(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId)
            .SetRawJsonValueAsync(jsonValue)
            .ContinueWithOnMainThread(task =>
            {
                if (!task.IsFaulted)
                {
                    Debug.Log("Added to database");
                    return;
                }

                Debug.Log("Error");
            });
    }

    public async Task<string> GetData(string path)
    {
        var snapshot = await FirebaseDatabase.DefaultInstance
            .GetReference(path)
            .GetValueAsync();

        return snapshot.Value.ToString();
    }

    public void UpdateData(ScoreEntry entry)
    {
        Debug.Log(entry.Score);
        Dictionary<string, object> update = new Dictionary<string, object>()
        {
            {$"/scores/{entry.Id}/Score", entry.Score}
        };
        databaseReference.UpdateChildrenAsync(update).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
                Debug.Log("Updated");
        });
    }

    public void GetScores()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("scores")
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    int counter = 1;

                    foreach (var dataSnapshot in task.Result.Children)
                    {
                        List<string> userInfo = new List<string>();

                        foreach (var child in dataSnapshot.Children)
                        {
                            userInfo.Add(child.Value.ToString());
                        }

                        var eventDispatcherService = ServiceLocator.Instance.GetService<IEventDispatcherService>();

                        var rankingEntry = new RankingEntry(counter.ToString(),
                            userInfo[0], userInfo[1], userInfo[2]);

                        eventDispatcherService.Dispatch<RankingEntry>(rankingEntry);
                        counter++;
                    }
                }
            });
    }
}