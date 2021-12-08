using System.Threading.Tasks;
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

     public void CheckExistingUser(string userId)
     {

          var eventDispatcherService = ServiceLocator.Instance.GetService<IEventDispatcherService>();

          CollectionReference usersRef = db.Collection("users");
          usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
          {
               
               QuerySnapshot snapshot = task.Result;
               foreach (DocumentSnapshot document in snapshot.Documents)
               {
                    if (document.Id == userId)
                    {
                         var user = document.ConvertTo<User>();
                         eventDispatcherService.Dispatch<string>(userId);
                         return;
                    }
               }

               eventDispatcherService.Dispatch<bool>(true);
          });
     }

     public void GetUserInfo(string userId)
     {
          CollectionReference usersRef = db.Collection("users");
          usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
          {
               QuerySnapshot snapshot = task.Result;
               foreach (DocumentSnapshot document in snapshot.Documents)
               {
                    if (document.Id == userId)
                    {
                         var user = document.ConvertTo<User>();
                         
                         var eventDispatcherService = ServiceLocator.Instance.GetService<IEventDispatcherService>();
                         eventDispatcherService.Dispatch<User>(user);
                    }
               }
          });
     }
     public void AddToDatabase(User newUser)
     {
          Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
          DocumentReference docRef = db.Collection("users").Document(newUser.Id);
          docRef.SetAsync(newUser);
     }
}