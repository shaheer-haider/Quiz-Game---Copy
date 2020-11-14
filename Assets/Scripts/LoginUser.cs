using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Firebase;
using Firebase.Auth;
using Proyecto26;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
public class LoginUser : MonoBehaviour
{

    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    FirebaseAuth auth;
    public string email;
    public string password;
    public TMP_Text warningText;
    public bool canGetScore;
    public int curScore;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        canGetScore = false;
    }

    private void Update()
    {
        if (canGetScore)
        {
            saveProfileData();
        }
    }
    public void authThenLoginUser()
    {
        email = emailField.text;
        password = passwordField.text;
        authWithFirebase();
    }
    public void authWithFirebase()
    {
        // auth.SendPasswordResetEmailAsync(email);
        // Firebase.Auth.Credential credential = Firebase.Auth.EmailAuthProvider.GetCredential(email, password);
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                warningText.text = "Process was canceled.";
                Invoke("hideWarning", 2f);
            }
            else if (task.IsFaulted)
            {
                warningText.text = "Error Occured. Please, restart app or check your network.";
                Invoke("hideWarning", 2f);
            }
            else if(task.IsCompleted)
            {
                canGetScore = true;
            }
        });
    }
    public void saveProfileData()
    {
        // print("logged in");
        canGetScore = false;
        // // InitResult
        RestClient.Get<User>("https://unityquizgame.firebaseio.com/" + email.Replace(".", "!") + ".json")
        .Then((response) =>
        {
            curScore = Convert.ToInt32(response.score);
        }).Finally(() =>
        {
            setPrefs();
        });

    }

    public void setPrefs()
    {
        PlayerPrefs.SetString("CurrentUser", email);
        PlayerPrefs.SetInt("CurrentUserScore", curScore);
        SceneManager.LoadScene("Quiz");
    }
    void hideWarning()
    {
        warningText.text = "";
    }

}