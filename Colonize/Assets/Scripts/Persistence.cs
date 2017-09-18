using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistence : MonoBehaviour 
{
    public static Persistence existing;
    public float Time { get; set; }

	void Awake () 
	{
        if (existing == null)
        {
            DontDestroyOnLoad(gameObject);
            existing = this;
        }
        else if (existing != this)
        {
            Destroy(gameObject);
        }
	}
}
