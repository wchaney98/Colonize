using System;
using System.Collections.Generic;
using System.Linq;

public class InputCommand
{
    public string KeyboardInput;
    public string ControllerInput;

    public event CheckInput CheckEvent;
    private CheckInput checkEvent;

    public delegate bool CheckInput();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inputString">axes, keycodes, input...</param>
    /// <param name="isController">controller control?</param>
    /// <param name="checkEvent">how to check if input is true</param>
    public InputCommand(string keyboardInput, string controllerInput, CheckInput checkEvent)
    {
        KeyboardInput = keyboardInput;
        ControllerInput = controllerInput;
        this.checkEvent = checkEvent;
        CheckEvent += this.checkEvent;
    }   

    public void ModifyCheck(CheckInput newCheckEvent)
    {
        CheckEvent -= checkEvent;
        checkEvent = newCheckEvent;
        CheckEvent += checkEvent;
    }

    public bool Check()
    {
        return CheckEvent.Invoke();
    }
}
