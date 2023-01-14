using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Register : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public InputField firstnameField;
    public InputField lastnameField;
    public TextMeshProUGUI text;

    public Button submitButton;

    private void Start()
    {
        //At the start of the program make the button uninteractable
        submitButton.interactable = false;
        text.text = " ";
        
    }

    public void CallRegister()
    {
        StartCoroutine(Registration());
    }

    IEnumerator Registration()
    {
        //Send the user's details from the input fields to the php to connect to the DB
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        form.AddField("firstname", firstnameField.text);
        form.AddField("lastname", lastnameField.text);
        //Connecting to the php stored on the website to connect to the DB
        WWW www = new WWW("https://projectstyx.000webhostapp.com/register.php",form);
        yield return www;
        
        if (www.text == "Registration successful")
        {
            //Once the user is created successfully as the message is sent back from the website change the scene to the main menu
            Debug.Log("User created succcessfully.");
            DBManager.username = usernameField.text;
            DBManager.firstname = firstnameField.text;
            DBManager.lastname = lastnameField.text;
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            //If account exists the inform the user
            Debug.Log("Account with this username already exists");
            text.text = "Account with this username already exists";
        }


    }

    public void InputValidation()
    {
        //Make sure the fields aren't empty before the user registers
        submitButton.interactable = (usernameField.text.Length >= 3 && passwordField.text.Length >= 6 && firstnameField.text.Length >= 3 && lastnameField.text.Length >= 3);
    }

}
