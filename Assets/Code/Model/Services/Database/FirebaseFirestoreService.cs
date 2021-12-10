using System;
using System.Threading.Tasks;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseFirestoreService : IDatabaseService
{
     private FirebaseFirestore db;

     public FirebaseFirestoreService()
     {
          db = FirebaseFirestore.DefaultInstance;
     }
     
     public async Task<bool> ExistKey(string collection, string key)
     {
          CollectionReference usersRef = db.Collection(collection);

          var snapshot = await usersRef.GetSnapshotAsync();
          
          foreach (DocumentSnapshot document in snapshot.Documents)
          {
               if (document.Id == key)
               {
                    return true;
               }
          }
          return false;
     }

     public async Task Save<T>(T userData, string collection, string key) where T : IUserData
     {
          DocumentReference docRef = db.Collection(collection).Document(key);
          await docRef.SetAsync(userData);
     }

     public async Task<T> Load<T>(string collection, string key)
     {
          var type = typeof(T);
          
          CollectionReference usersRef = db.Collection(collection);
          var snapshot = await usersRef.GetSnapshotAsync();
          
          foreach (DocumentSnapshot document in snapshot.Documents)
          {
               if (document.Id == key)
               {
                    var user = document.ConvertTo<T>();
                    return user;
               }
          }

          throw new Exception($"Can't find {key}");
     }

    /* public void CheckExistingUser(string userId)
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
     public void AddToDatabase(User User)
     {
          Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
          DocumentReference docRef = db.Collection("users").Document(User.Id);
          docRef.SetAsync(User);
     }*/
}