﻿using System.Collections;
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
        textComponent = textObject.GetComponent<Text>();

        StringBuilder s = new StringBuilder();
        s.AppendLine("GAME OVER");
        s.AppendLine("---");
        s.AppendLine("YOU SURVIVED FOR...");
        s.AppendLine(Persistence.existing.Time.ToString());
        if (Persistence.existing.Time > 120)
            s.AppendLine("<color=magenta>THAT WAS INSANE!!!</color>");
        else if (Persistence.existing.Time > 90)
            s.AppendLine("<color=teal>NICE JOB</color>");
        else if (Persistence.existing.Time > 60)
            s.AppendLine("<color=grey>NOT BAD...</color>");
        else
            s.AppendLine("<color=red>ARE YOU EVEN TRYING</color>");

        textComponent.text = s.ToString();
    }

    public void Retry()
    {
        Persistence.existing.Time = 0;
        SceneManager.LoadScene("Main Game");
    }
}