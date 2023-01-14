using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //Script to allow changing from one scene to the other
    public void FlowChart1()
    {
        SceneManager.LoadScene("Flowchart 1");
    }
    public void FlowChart2()
    {
        SceneManager.LoadScene("Flowchart 2");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Register()
    {
        SceneManager.LoadScene("Register");
    }
    public void Login()
    {
        SceneManager.LoadScene("Login");
    }
    public void Profile()
    {
        SceneManager.LoadScene("Profile");
    }
}
