using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
    public void Play()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
