using System.Collections.Generic;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseFirestoreService
{
     private FirebaseFirestore db;

     public FirebaseFirestoreService()
     {
          db = FirebaseFirestore.DefaultInstance;
     }

     public User GetData(string userId)
     {
          User user = new User();
          
          CollectionReference usersRef = db.Collection("users");
          usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
          {
               QuerySnapshot snapshot = task.Result;
               foreach (DocumentSnapshot document in snapshot.Documents)
               {
                    if (document.Id == userId)
                    {
                         user = document.ConvertTo<User>();
                    }
               }
          });

          return user;
     }
     
     public void AddToDatabase(User newUser)
     {
          Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
          DocumentReference docRef = db.Collection("users").Document(newUser.Id);
          docRef.SetAsync(newUser);
     }
}