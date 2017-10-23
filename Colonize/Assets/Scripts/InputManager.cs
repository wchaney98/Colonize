using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonBehavior<InputManager>
{
    public InputCommand ConnectKey;
    public InputCommand LevelUpKey;
    public InputCommand SlowTimeKey;
    public InputCommand SurplusKey;
    public InputCommand DestroyVirusesKey;
    public InputCommand TenTimesResourcesKey;

    public InputCommand ControllerSelectClosest;
    public InputCommand ControllerMoveSelected;

    public int SelectedLayout;

    public List<InputCommand> GetCommands()
    {
        return commands;
    }

    public void ApplyLayout1()
    {
        ConnectKey.ControllerInput = "J_X";

        LevelUpKey.ControllerInput = "J_Y";

        ControllerSelectClosest.ControllerInput = "J_A";
        ControllerSelectClosest.ModifyCheck();

        ControllerMoveSelected.ControllerInput = "J_Triggers";
        ControllerMoveSelected.ModifyCheck(() =>
        {
            return Input.GetAxis(ControllerMoveSelected.ControllerInput) < -0.1f;
        });

        SelectedLayout = 1;
    }

    public void ApplyLayout2()
    {
        ConnectKey.ControllerInput = "J_X";

        LevelUpKey.ControllerInput = "J_Y";

        // Right trigger
        ControllerSelectClosest.ControllerInput = "J_Triggers";
        ControllerSelectClosest.ModifyCheck(() =>
        {
            return Input.GetAxis(ControllerSelectClosest.ControllerInput) < -0.1f;
        });

        // Left trigger
        ControllerMoveSelected.ControllerInput = "J_Triggers";
        ControllerMoveSelected.ModifyCheck(() =>
        {
            return Input.GetAxis(ControllerMoveSelected.ControllerInput) > 0.1f;
        });

        SelectedLayout = 2;
    }

    public void ApplyLayout3()
    {
        ConnectKey.ControllerInput = "J_RBumper";

        LevelUpKey.ControllerInput = "J_Y";

        ControllerSelectClosest.ControllerInput = "J_X";
        ControllerSelectClosest.ModifyCheck();

        ControllerMoveSelected.ControllerInput = "J_Triggers";
        ControllerMoveSelected.ModifyCheck(() =>
        {
            return Input.GetAxis(ControllerMoveSelected.ControllerInput) < -0.1f;
        });

        SelectedLayout = 3;
    }

    private List<InputCommand> commands = new List<InputCommand>();

    protected override void Init()
    {
        ConnectKey = new InputCommand(commands, "c", "J_X");
        LevelUpKey = new InputCommand(commands, "l", "J_Y");

        SlowTimeKey = new InputCommand(commands, "r", "J_DpadHorizontal", () =>
        {
            return Input.GetKeyDown(SlowTimeKey.KeyboardInput) ||
            Input.GetAxis(SlowTimeKey.ControllerInput) == -1;
        });
        SurplusKey = new InputCommand(commands, "t", "J_DpadHorizontal", () =>
        {
            return Input.GetKeyDown(SurplusKey.KeyboardInput) ||
            Input.GetAxis(SurplusKey.ControllerInput) == 1;
        });
        DestroyVirusesKey = new InputCommand(commands, "f", "J_DpadVertical", () =>
        {
            return Input.GetKeyDown(DestroyVirusesKey.KeyboardInput) ||
            Input.GetAxis(DestroyVirusesKey.ControllerInput) == 1;
        });
        TenTimesResourcesKey = new InputCommand(commands, "g", "J_DpadVertical", () =>
        {
            return Input.GetKeyDown(TenTimesResourcesKey.KeyboardInput) ||
            Input.GetAxis(TenTimesResourcesKey.ControllerInput) == -1;
        });

        ControllerSelectClosest = new InputCommand(commands, "", "J_A", () =>
        {
            return Input.GetButtonDown(ControllerSelectClosest.ControllerInput);
        });
        ControllerMoveSelected = new InputCommand(commands, "", "J_Triggers", () =>
        {
            return Input.GetAxis(ControllerMoveSelected.ControllerInput) < -0.1f;
        });

        SelectedLayout = 1;
    }

    private void Update()
    {

    }
}
