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
}