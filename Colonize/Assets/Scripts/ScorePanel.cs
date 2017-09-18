using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour 
{
    public Text textObj;
	void Start () 
	{
		
	}
	
	void Update () 
	{
        textObj.text = "TIME: " + Persistence.existing.Time;
	}
}
