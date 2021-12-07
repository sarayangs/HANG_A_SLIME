using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseDatabaseService
{
        private DatabaseReference databaseReference;

        public FirebaseDatabaseService()
        {
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        }

        public void AddData(int score)
        {
                var jsonValue = JsonUtility.ToJson(new ScoreEntry((score)));

                databaseReference
                        .Child("scores")
                        .Child(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId)
                        .SetRawJsonValueAsync(jsonValue)
                        .ContinueWithOnMainThread(task =>
                        {
                                if (!task.IsFaulted) {
                                        Debug.Log("Added to database");
                                        return;
                                }
                                Debug.Log("Error");
                        });
        }
}