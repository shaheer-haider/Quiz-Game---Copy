using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Proyecto26;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Text.RegularExpressions;
public class SignUpManager : MonoBehaviour
{

    DatabaseReference reference;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningText;
    private string email;
    private string password;
    FirebaseAuth auth;
    bool canCreate = false;


    private void Start()
    {
        if (!(PlayerPrefs.HasKey("CurrentUser") && PlayerPrefs.HasKey("CurrentUserScore")))
        {
            PlayerPrefs.SetString("CurrentUser", "None");
            PlayerPrefs.SetInt("CurrentUserScore", 0);
        }
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unityquizgame.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    public void signUp()
    {
        parseThenCreate();
    }
    public void createAccount()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                warningText.text = "Process was canceled";
                Invoke("hideWarning", 2f);
                return;
            }
            if (task.IsFaulted)
            {
                warningText.text = "Error Occured. Please, restart app or check your network.";
                Invoke("hideWarning", 2f);
                return;
            }
            warningText.text = "";
            var username = email.Replace(".", "!");
            User user = new User("0");
            string json = JsonUtility.ToJson(user);
            canCreate = false;
            reference.Child(username).SetRawJsonValueAsync(json);
        });
    }

    void parseThenCreate()
    {
        email = emailRegisterField.text;
        password = passwordRegisterField.text;
        var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        if (Regex.IsMatch(email, regex, RegexOptions.IgnoreCase))
        {
            // password check
            if (password == passwordRegisterVerifyField.text)
            {
                // password length check
                if (password.Length > 5)
                {
                    // check if user already exists
                    RestClient.Get<User>("https://unityquizgame.firebaseio.com/" + email.Replace(".", "!") + ".json")
                    .Then((response) =>
                    {

                    })
                    .Catch((err) =>
                    {
                        canCreate = true;
                        createAccount();
                    })
                    .Done(() =>
                    {
                        if (!canCreate)
                        {
                            warningText.text = "Email already exists";
                            Invoke("hideWarning", 2f);
                        }
                    });
                }
                else
                {
                    warningText.text = "Minimum Password Length is 5";
                    Invoke("hideWarning", 2f);
                }
            }
            else
            {
                warningText.text = "Passwords not matched";
                Invoke("hideWarning", 2f);

            }
        }
        else
        {
            warningText.text = "Invalid Email";
            Invoke("hideWarning", 2f);
        }
    }
    void hideWarning()
    {
        warningText.text = "";
    }


}

