using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Profile : MonoBehaviour
{
    //Variable Declaration
    public InputField passwordField;
    public InputField oldpasswordField;
    public TextMeshProUGUI debugtext;
    public TextMeshProUGUI username;
    public TextMeshProUGUI firstname;
    public TextMeshProUGUI lastname;
    public Button submitButton;

    private void Start()
    {
        //At the start of the program make the button uninteractable and set the username, firstname, and lastname labels to the current 
        submitButton.interactable = false;
        debugtext.text = " ";
        username.text = DBManager.username;
        firstname.text = DBManager.firstname;
        lastname.text = DBManager.lastname;
        

    }


    public void CallChangePassword()
    {
        StartCoroutine(PasswordChange());
    }

    IEnumerator PasswordChange()
    {
        //Send the current username, password, and current password from the input fields to the php to connect to the DB
        WWWForm form = new WWWForm();
        form.AddField("username", DBManager.username);
        form.AddField("password", passwordField.text);
        form.AddField("oldpassword", oldpasswordField.text);
        //Connecting to the php stored on the website
        WWW www = new WWW("https://projectstyx.000webhostapp.com/change.php", form);
        yield return www;

        //If the change is successful send a message to the user
        if (www.text == "Password change successful")
        {
            Debug.Log("Password changed successfully.");
            debugtext.text = www.text;
            
        }
        //If not, give the user an error message
        else
        {
            Debug.Log("Password change failed");
            debugtext.text = www.text;
        }



    }
    //Make sure the password fields aren't empty before submitting
    public void InputValidation()
    {
        //Make sure the fields aren't empty before the user registers
        submitButton.interactable = (passwordField.text.Length >= 6 && oldpasswordField.text.Length >= 6);
    }
}
