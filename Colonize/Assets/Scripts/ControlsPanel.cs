using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ControlsPanel : MonoBehaviour
{

    private void Start()
    {
        Text text = GetComponentInChildren<Text>();
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Controls: ")
          .AppendLine()
          .AppendLine("Connect: " + InputManager.Instance.ConnectKey.KeyboardInput)
          .AppendLine("SlowTime: " + InputManager.Instance.SlowTimeKey.KeyboardInput)
          .AppendLine("Surplus: " + InputManager.Instance.SurplusKey.KeyboardInput)
          .AppendLine("Destroy Viruses: " + InputManager.Instance.DestroyVirusesKey.KeyboardInput)
          .AppendLine("10X Resources: " + InputManager.Instance.TenTimesResourcesKey.KeyboardInput);

        text.text = sb.ToString();
    }

    private void OnEnable()
    {
        Start();
    }
}
