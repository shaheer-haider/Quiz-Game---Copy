using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    public GameObject loginPanel;
    public GameObject signUpPanel;
    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }
    public void showLogin(){
        signUpPanel.SetActive(false);
        loginPanel.SetActive(true);
    }
    public void showSignUp(){
        loginPanel.SetActive(false);
        signUpPanel.SetActive(true);
    }
}
