using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
    EventSystem es;
    GameObject playButton;

    private void Start()
    {
        Time.timeScale = 1f;
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        playButton = GameObject.Find("Play");

        if (!Persistence.Instance.CheckControllerConnection())
            es.firstSelectedGameObject = null;
        else
            es.firstSelectedGameObject = playButton;
    }

    private void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void Options()
    {
        SoundManager.Instance.DoPlayOneShot(SoundFile.ButtonClick);
        if (Persistence.Instance.ControllerIsConnected)
            SceneManager.LoadScene("Controller Options");
        else
            SceneManager.LoadScene("Options");
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
