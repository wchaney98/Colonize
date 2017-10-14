using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager existing;
    public float Time { get; set; }
    public bool TenTimesAbilityActive { get; set; }

    public Dictionary<string, string> Controls;

    void Awake()
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
