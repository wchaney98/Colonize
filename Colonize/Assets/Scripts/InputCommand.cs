using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputCommand
{
    public string KeyboardInput;
    public string ControllerInput;

    public delegate bool CheckInput();

    public event CheckInput CheckEvent;
    private CheckInput checkEvent;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="commands">list of commands</param>
    /// <param name="inputString">axes, keycodes, input...</param>
    /// <param name="isController">controller control?</param>
    /// <param name="checkEvent">how to check if input is true</param>
    public InputCommand(List<InputCommand> commands, string keyboardInput, string controllerInput, CheckInput checkEvent)
    {
        KeyboardInput = keyboardInput;
        ControllerInput = controllerInput;
        this.checkEvent = checkEvent;
        CheckEvent += this.checkEvent;
        commands.Add(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="commands">list of commands</param>
    /// <param name="inputString">axes, keycodes, input...</param>
    /// <param name="isController">controller control?</param>
    /// <param name="checkEvent">how to check if input is true</param>
    public InputCommand(List<InputCommand> commands, string keyboardInput, string controllerInput)
    {
        KeyboardInput = keyboardInput;
        ControllerInput = controllerInput;
        checkEvent = new CheckInput(() =>
        {
            return Input.GetKeyDown(KeyboardInput) ||
            Input.GetButtonDown(ControllerInput);
        });
        CheckEvent += checkEvent;
        commands.Add(this);
    }

    public void ModifyCheck(CheckInput newCheckEvent)
    {
        CheckEvent -= checkEvent;
        checkEvent = newCheckEvent;
        CheckEvent += checkEvent;
    }

    public void ModifyCheck()
    {
        CheckEvent -= checkEvent;
        checkEvent = new CheckInput(() =>
        {
            return KeyboardInput.Length > 0 ? Input.GetKeyDown(KeyboardInput) : false ||
            Input.GetButtonDown(ControllerInput);
        });
        CheckEvent += checkEvent;
    }

    public bool Check()
    {
        return CheckEvent.Invoke();
    }
}
