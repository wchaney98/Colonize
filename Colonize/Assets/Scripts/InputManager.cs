using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonBehavior<InputManager>
{
    InputCommand AKey;
    
    private void Start()
    {
        AKey = new InputCommand("a", "J_A", () =>
        {
            return Input.GetKeyDown(AKey.KeyboardInput) ||
                   Input.GetButtonDown(AKey.ControllerInput);
        });
        AKey.KeyboardInput = "b";
        AKey.ModifyCheck(() =>
        {
            return Input.GetKeyUp(AKey.KeyboardInput) ||
                   Input.GetButtonDown(AKey.ControllerInput);
        });
    }

    private void Update()
    {
        Debug.Log(AKey.Check());
    }

    private bool CheckAKey()
    {
        return Input.GetKeyDown(KeyCode.A);
    }
}
