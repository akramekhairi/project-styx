using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Login : MonoBehaviour
{
    //Variable Declaration
    public InputField usernameField;
    public InputField passwordField;
    public TextMeshProUGUI text;
    public Button submitButton;

    private void Start()
    {
        //At the start of the program make the button uninteractable
        submitButton.interactable = false;
        text.text = " ";
        
    }

    public void CallLogin()
    {
        StartCoroutine(SignIn());
    }

    IEnumerator SignIn()
    {
        //Send the username and password from the input fields to the php to connect to the DB
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        //Connecting to the php stored on the website to connect to the DB
        WWW www = new WWW("https://projectstyx.000webhostapp.com/login.php", form);
        yield return www;
        //Check if the php returns a successful login then go to main menu and set the static DBManager values
        if (www.text.Split('\n')[0] == "Login successful")
        {
            Debug.Log("Logged in succcessfully.");
            DBManager.username = usernameField.text;
            DBManager.firstname = www.text.Split('\n')[1];
            DBManager.lastname = www.text.Split('\n')[2];
            SceneManager.LoadScene("Main Menu");
        }
        //Otherwise send an error to the user
        else
        {
            Debug.Log("No account with this username exists");
            text.text = www.text;
        }

        

    }


    public void InputValidation()
    {
        //Make sure the fields aren't empty before the user logs in
        submitButton.interactable = (usernameField.text.Length >= 3 && passwordField.text.Length >= 6);
    }
}
