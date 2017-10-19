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
                foreach (var key in Persistence.Instance.Controls)
                {
                    if (key.Value == Input.inputString)
                    {
                        marker = key.Key;
                    }
                }
                Persistence.Instance.Controls[marker] = "";

                currentButtonText.text = Input.inputString;

                // :(
                if (currentButtonText == ConnectKeyButtonText)
                {
                    Persistence.Instance.Controls["ConnectKey"] = Input.inputString;
                }
                else if (currentButtonText == LevelUpKeyButtonText)
                {
                    Persistence.Instance.Controls["LevelUpKey"] = Input.inputString;
                }
                else if (currentButtonText == SlowTimeKeyButtonText)
                {
                    Persistence.Instance.Controls["SlowTimeKey"] = Input.inputString;
                }
                else if (currentButtonText == SurplusKeyButtonText)
                {
                    Persistence.Instance.Controls["SurplusKey"] = Input.inputString;
                }
                else if (currentButtonText == DestroyVirusesKeyButtonText)
                {
                    Persistence.Instance.Controls["DestroyVirusesKey"] = Input.inputString;
                }
                else if (currentButtonText == TenTimesResourceKeyButtonText)
                {
                    Persistence.Instance.Controls["TenTimesResourceKey"] = Input.inputString;
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

        ConnectKeyText.text = Persistence.Instance.Controls["ConnectKey"];
        LevelUpKeyText.text = Persistence.Instance.Controls["LevelUpKey"];
        SlowTimeKeyText.text = Persistence.Instance.Controls["SlowTimeKey"];
        SurplusKeyText.text = Persistence.Instance.Controls["SurplusKey"];
        DestroyVirusesKeyText.text = Persistence.Instance.Controls["DestroyVirusesKey"];
        TenTimesResourceKeyText.text = Persistence.Instance.Controls["TenTimesResourceKey"];
    }

    void UpdateStrings()
    {
        ConnectKeyText.text = Persistence.Instance.Controls["ConnectKey"];
        LevelUpKeyText.text = Persistence.Instance.Controls["LevelUpKey"];
        SlowTimeKeyText.text = Persistence.Instance.Controls["SlowTimeKey"];
        SurplusKeyText.text = Persistence.Instance.Controls["SurplusKey"];
        DestroyVirusesKeyText.text = Persistence.Instance.Controls["DestroyVirusesKey"];
        TenTimesResourceKeyText.text = Persistence.Instance.Controls["TenTimesResourceKey"];
    }
}
