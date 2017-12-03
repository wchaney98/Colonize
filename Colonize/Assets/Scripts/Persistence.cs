using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistence : SingletonBehavior<Persistence>
{
    public float Time { get; set; }
    public bool TenTimesAbilityActive { get; set; }
    public bool ControllerIsConnected { get; set; }

    public Dictionary<string, string> Controls;
    public Dictionary<string, string> ControllerControls;

    // High roller
    public int LifeGathered = 0;

    // Medicine
    public int LifeLeeched = 0;

    // Daily fix
    public int RoundsPlayed = 0;

    // Most wanted
    public bool NodeSurvivedHundredSeconds = false;

    // Wall
    public int VirusesStopped = 0;

    public Vector2 GetMainStickData()
    {
        return new Vector2(Input.GetAxis("J_MainHorizontal"),
                           Input.GetAxis("J_MainVertical"));
    }

    protected override void Init()
    {
        Controls = new Dictionary<string, string>
            {
                { "ConnectKey", "c" },
                { "LevelUpKey", "l" },
                { "SlowTimeKey", "r" },
                { "SurplusKey", "t" },
                { "DestroyVirusesKey", "f" },
                { "TenTimesResourceKey", "g" },
            };

        ControllerControls = new Dictionary<string, string>
            {
                { "ConnectKey", "J_X" },
                { "LevelUpKey", "J_Y" },
                { "SelectClosest", "J_A" },
                { "MoveSelected", "J_Triggers" },
            };
    }

    private void Start()
    {
        CheckControllerConnection();
    }

    private void Update()
    {
        CheckControllerConnection();
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            // High roller
            LifeGathered++;

            // Medicine
            LifeLeeched++;

            // Daily fix
            RoundsPlayed++;

            // Most wanted
            NodeSurvivedHundredSeconds = !NodeSurvivedHundredSeconds;

            // Wall
            VirusesStopped++;
        }
    }

    public bool CheckControllerConnection()
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
        return ControllerIsConnected;
    }
}
