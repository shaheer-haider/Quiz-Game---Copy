using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class PlayerScore : MonoBehaviour
{
    DatabaseReference reference;
    public string result;
    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unityquizgame.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void onSubmit()
    {
        writeNewUser("shaheerhaider00000", "100", "secret");
    }

    public void getFromDatabase()
    {
        RestClient.Get<User>("https://unityquizgame.firebaseio.com/" + "shaheerhaider00000" + ".json").Then(
            (response) =>
            {
                print(response.score);
            });
    }

    private void writeNewUser(string email, string score, string password)
    {
        User user = new User(score);
        string json = JsonUtility.ToJson(user);
        reference.Child(email).SetRawJsonValueAsync(json);
    }
}
