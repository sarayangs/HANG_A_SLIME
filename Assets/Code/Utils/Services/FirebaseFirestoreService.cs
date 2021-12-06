using System.Collections.Generic;
using Firebase.Firestore;
using Firebase.Extensions;

public class FirebaseFirestoreService
{
     private FirebaseFirestore db;

     public FirebaseFirestoreService()
     {
          db = FirebaseFirestore.DefaultInstance;
     }

     public void GetData()
     {
          CollectionReference usersRef = db.Collection("users");
          usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
          {
               QuerySnapshot snapshot = task.Result;
               foreach (DocumentSnapshot document in snapshot.Documents)
               {
                    Dictionary<string, object> documentDictionary = document.ToDictionary();
                    
                    if (documentDictionary.ContainsKey("Middle"))
                    {
                    }

               }
          });
     }
     
     public void AddToDatabase(User newUser)
     {
          Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
          DocumentReference docRef = db.Collection("users").Document(auth.CurrentUser.UserId);
          docRef.SetAsync(newUser);
     }
}