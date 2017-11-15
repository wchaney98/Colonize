using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerControlsPanel : MonoBehaviour
{

    private void Start()
    {
        if (Persistence.Instance.ControllerIsConnected && GameObject.FindObjectOfType(typeof(ControllerCursor)) != null)
        {
            gameObject.SetActive(true);
            Image image = GetComponent<Image>();
            switch (InputManager.Instance.SelectedLayout)
            {
                case 1:
                    image.sprite = Resources.Load<Sprite>("Graphics/Layout1");
                    break;
                case 2:
                    image.sprite = Resources.Load<Sprite>("Graphics/Layout2");
                    break;
                case 3:
                    image.sprite = Resources.Load<Sprite>("Graphics/Layout3");
                    break;
            }
        }
        else
        {
            gameObject.SetActive(false);

        }
    }

    private void OnEnable()
    {
        Start();
    }
}
