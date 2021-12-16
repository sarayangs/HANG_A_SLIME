using UnityEngine;

public class FirebaseMessagingService : IMessagingService
{
    public void ActivateMessaging()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        
        Debug.Log("Activate messaging");
    }

    public void DeactivateMessaging()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived += null;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += null;
        Debug.Log("Deactivate messaging");
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token) {
        Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e) {
        Debug.Log("Received a new message from: " + e.Message.From);
    }
}