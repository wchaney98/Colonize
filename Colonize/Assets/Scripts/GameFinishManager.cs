using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFinishManager : MonoBehaviour 
{
    public GameObject textObject;

    private Text textComponent;
    void Start () 
    {
        Persistence.Instance.RoundsPlayed++;

        Time.timeScale = 1f;
        textComponent = textObject.GetComponent<Text>();

        StringBuilder s = new StringBuilder();
        s.AppendLine("GAME OVER");
        s.AppendLine("---");
        s.AppendLine("YOU SURVIVED FOR...");
        s.AppendLine(Persistence.Instance.Time.ToString());
        if (Persistence.Instance.Time > 120)
            s.AppendLine("<color=magenta>THAT WAS INSANE!!!</color>");
        else if (Persistence.Instance.Time > 90)
            s.AppendLine("<color=teal>NICE JOB</color>");
        else if (Persistence.Instance.Time > 60)
            s.AppendLine("<color=grey>NOT BAD...</color>");
        else
            s.AppendLine("<color=red>ARE YOU EVEN TRYING</color>");

        textComponent.text = s.ToString();
    }

    public void Retry()
    {
        Persistence.Instance.Time = 0;
        SceneManager.LoadScene("Main Game");
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
