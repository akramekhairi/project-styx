using UnityEngine;
using TMPro;

public class ModelDialog : MonoBehaviour
{
    //This script is attached to the pop up dialog to add the pop up and closing functionality to it

    //Variable declaration (public makes them accessible from the unity editor to allow us to hook up the assets we wish to change from the editor)
    public TextMeshProUGUI text;
    public GameObject gameobject;

    //public so it can be accessed by the AR Manager but we hide it from the inspector as there is no need for it to be accessible to the editor
    [HideInInspector]
    public bool isVisible = true;

    public void Start()
    {
        //Start is called at the beginning of runtime and the pop up is set to be invisible at the beginning
        SetVisibility(false);
    }

    public void SetVisibility(bool pIsVisible)
    {

        //This sets the objects visibility to false or true and changes the isVisible variable so it can be accessed from the AR Manager
        //to know whether to open a new pop up or not allow the user to open another one until they close the current one
        gameobject.SetActive(pIsVisible);
        isVisible = pIsVisible;
    }

    public void Set(string pMessage)
    {
        //This sets the pop up to be visible and assigns it text to display from the AR Manager
        SetVisibility(true);
        text.text = pMessage;

    }

    public void OnClose()
    {
        //This is applied to the close button and cause the pop up to become invisible or closed
        SetVisibility(false);
    }
}
