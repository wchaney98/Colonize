using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject ConnectKey;
    public GameObject LevelUpKey;
    public GameObject SlowTimeKey;
    public GameObject SurplusKey;
    public GameObject DestroyVirusesKey;
    public GameObject TenTimesResourceKey;

    private Text ConnectKeyText;
    private Text LevelUpKeyText;
    private Text SlowTimeKeyText;
    private Text SurplusKeyText;
    private Text DestroyVirusesKeyText;
    private Text TenTimesResourceKeyText;

    private Text ConnectKeyButtonText;
    private Text LevelUpKeyButtonText;
    private Text SlowTimeKeyButtonText;
    private Text SurplusKeyButtonText;
    private Text DestroyVirusesKeyButtonText;
    private Text TenTimesResourceKeyButtonText;

    private bool waitingForNewKey = false;
    private Text currentButtonText = null;

    private void EditButtonPressed(Text buttonText)
    {
        waitingForNewKey = true;
        currentButtonText = buttonText;
    }

    private void Update()
    {
        if (waitingForNewKey)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            currentButtonText.text = "Press any key...";

            if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2) && Input.anyKeyDown)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;          
                string marker = "";
                foreach (InputCommand command in InputManager.Instance.GetCommands())
                {
                    if (command.KeyboardInput == Input.inputString)
                    {
                        command.KeyboardInput = "";
                    }
                }

                currentButtonText.text = Input.inputString;

                // :(
                if (currentButtonText == ConnectKeyButtonText)
                {
                    InputManager.Instance.ConnectKey.KeyboardInput = Input.inputString;
                }
                else if (currentButtonText == LevelUpKeyButtonText)
                {
                    InputManager.Instance.LevelUpKey.KeyboardInput = Input.inputString;
                }
                else if (currentButtonText == SlowTimeKeyButtonText)
                {
                    InputManager.Instance.SlowTimeKey.KeyboardInput = Input.inputString;
                }
                else if (currentButtonText == SurplusKeyButtonText)
                {
                    InputManager.Instance.SurplusKey.KeyboardInput = Input.inputString;
                }
                else if (currentButtonText == DestroyVirusesKeyButtonText)
                {
                    InputManager.Instance.DestroyVirusesKey.KeyboardInput = Input.inputString;
                }
                else if (currentButtonText == TenTimesResourceKeyButtonText)
                {
                    InputManager.Instance.TenTimesResourcesKey.KeyboardInput = Input.inputString;
                }

                waitingForNewKey = false;
                currentButtonText.text = "Edit key...";
                UpdateStrings();
            }
        }
    }

    public void EditConnectKey()
    {
        EditButtonPressed(ConnectKeyButtonText);
    }
    public void EditLevelUpKey()
    {
        EditButtonPressed(LevelUpKeyButtonText);
    }
    public void EditSlowTimeKey()
    {
        EditButtonPressed(SlowTimeKeyButtonText);
    }
    public void EditSurplusKey()
    {
        EditButtonPressed(SurplusKeyButtonText);
    }
    public void EditDestroyVirusesKey()
    {
        EditButtonPressed(DestroyVirusesKeyButtonText);
    }
    public void EditTenTimesResourceKey()
    {
        EditButtonPressed(TenTimesResourceKeyButtonText);
    }

    void Start()
    {
        ConnectKeyText = ConnectKey.transform.Find("Key").GetComponent<Text>();
        LevelUpKeyText = LevelUpKey.transform.Find("Key").GetComponent<Text>();
        SlowTimeKeyText = SlowTimeKey.transform.Find("Key").GetComponent<Text>();
        SurplusKeyText = SurplusKey.transform.Find("Key").GetComponent<Text>();
        DestroyVirusesKeyText = DestroyVirusesKey.transform.Find("Key").GetComponent<Text>();
        TenTimesResourceKeyText = TenTimesResourceKey.transform.Find("Key").GetComponent<Text>();

        // Text is the name of the Text object under the Edit key button
        ConnectKeyButtonText = ConnectKey.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        LevelUpKeyButtonText = LevelUpKey.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        SlowTimeKeyButtonText = SlowTimeKey.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        SurplusKeyButtonText = SurplusKey.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        DestroyVirusesKeyButtonText = DestroyVirusesKey.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        TenTimesResourceKeyButtonText = TenTimesResourceKey.transform.GetChild(2).GetChild(0).GetComponent<Text>();

        UpdateStrings();
    }

    void UpdateStrings()
    {
        ConnectKeyText.text = InputManager.Instance.ConnectKey.KeyboardInput;
        LevelUpKeyText.text = InputManager.Instance.LevelUpKey.KeyboardInput;
        SlowTimeKeyText.text = InputManager.Instance.SlowTimeKey.KeyboardInput;
        SurplusKeyText.text = InputManager.Instance.SurplusKey.KeyboardInput;
        DestroyVirusesKeyText.text = InputManager.Instance.DestroyVirusesKey.KeyboardInput;
        TenTimesResourceKeyText.text = InputManager.Instance.TenTimesResourcesKey.KeyboardInput;
    }
}
