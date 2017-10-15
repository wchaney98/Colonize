using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistence : MonoBehaviour 
{
    public static Persistence existing;
    public float Time { get; set; }
    public bool TenTimesAbilityActive { get; set; }
    public bool ControllerIsConnected { get; set; }
    
    public Dictionary<string, string> Controls;
    public Dictionary<string, string> ControllerControls;

    void Awake ()
    { 
        if (existing == null)
        {
            DontDestroyOnLoad(gameObject);
            existing = this;

            // load this from a file later...
            Controls = new Dictionary<string, string>
            {
                { "ConnectKey", "c" },
                { "LevelUpKey", "l" },
                { "SlowTimeKey", "r" },
                { "SurplusKey", "t" },
                { "DestroyVirusesKey", "f" },
                { "TenTimesResourceKey", "g" },
                { "ProtanopeToggle", "F1" },
                { "DeuteranopeToggle", "F2" }
            };

            ControllerControls = new Dictionary<string, string>
            {
                { "ConnectKey", "c" },
                { "LevelUpKey", "l" },
                { "SlowTimeKey", "r" },
                { "SurplusKey", "t" },
                { "DestroyVirusesKey", "f" },
                { "TenTimesResourceKey", "g" },
                { "ProtanopeToggle", "F1" },
                { "DeuteranopeToggle", "F2" },
                { "SelectClosest", "J_Y" }
            };
        }
        else if (existing != this)
        {
            Destroy(gameObject);
        }
	}

    private void Update()
    {
        string[] temp = Input.GetJoystickNames();
        if (temp.Length > 0)
        {
            if (!string.IsNullOrEmpty(temp[0]))
            {
                if (ControllerIsConnected == false)
                    Debug.Log("Controller is connected");
                ControllerIsConnected = true;
            }
            else
            {
                if (ControllerIsConnected == true)
                    Debug.Log("Controller not connected");
                ControllerIsConnected = false;
            }
        }
    }
}
