using System.Collections.Generic;
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

        public void AddData(int score, string name)
        {
                var jsonValue = JsonUtility.ToJson(new ScoreEntry(score, name));

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

        public void GetScores()
        {
                FirebaseDatabase.DefaultInstance
                        .GetReference("scores")
                        .GetValueAsync()
                        .ContinueWithOnMainThread(task => {
                                if (task.IsCompleted) {
                                        
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
                                                 
                                                Debug.Log("hi");
                                                eventDispatcherService.Dispatch<RankingEntry>(rankingEntry);
                                                counter++;
                                        }
                                }
                        });
        }
        
}