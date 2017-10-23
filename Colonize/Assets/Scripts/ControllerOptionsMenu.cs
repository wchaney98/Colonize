using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerOptionsMenu : MonoBehaviour
{
    public GameObject Layout1;
    public GameObject Layout2;
    public GameObject Layout3;

    GameObject objectSelectedLastFrame = null;
    EventSystem eventSystem;

    GameObject layouts;
    RectTransform layoutsRect;
    iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
    float animationTime = 0.5f;

    void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        layouts = GameObject.Find("Layouts");
        layoutsRect = layouts.GetComponent<RectTransform>();
        GameObject.Find("Layout" + InputManager.Instance.SelectedLayout).GetComponent<Image>().color = Color.green;
    }

    void Update()
    {
        if (objectSelectedLastFrame != eventSystem.currentSelectedGameObject)
        {
            objectSelectedLastFrame = eventSystem.currentSelectedGameObject;
            if (objectSelectedLastFrame == Layout1)
            {
                iTween.ValueTo(layoutsRect.gameObject, iTween.Hash(
                    "from", layoutsRect.anchoredPosition,
                    "to", Vector2.zero,
                    "time", animationTime,
                    "onupdatetarget", gameObject,
                    "onupdate", "MoveGuiElement",
                    "easetype", easeType));
            }
            else if (objectSelectedLastFrame == Layout2)
            {
                iTween.ValueTo(layoutsRect.gameObject, iTween.Hash(
                    "from", layoutsRect.anchoredPosition,
                    "to", new Vector2(-700, 0),
                    "time", animationTime,
                    "onupdatetarget", gameObject,
                    "onupdate", "MoveGuiElement",
                    "easetype", easeType));
            }
            else if (objectSelectedLastFrame == Layout3)
            {
                iTween.ValueTo(layoutsRect.gameObject, iTween.Hash(
                    "from", layoutsRect.anchoredPosition,
                    "to", new Vector2(-1400, 0),
                    "time", animationTime,
                    "onupdatetarget", gameObject,
                    "onupdate", "MoveGuiElement",
                    "easetype", easeType));
            }
        }
    }

    public void SwitchToLayout1()
    {
        InputManager.Instance.ApplyLayout1();
        Layout1.GetComponent<Image>().color = Color.green;
        Layout2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.748f);
        Layout3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.748f);

    }

    public void SwitchToLayout2()
    {
        InputManager.Instance.ApplyLayout2();
        Layout1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.748f);
        Layout2.GetComponent<Image>().color = Color.green;
        Layout3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.748f);
    }

    public void SwitchToLayout3()
    {
        InputManager.Instance.ApplyLayout3();
        Layout1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.748f);
        Layout2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.748f);
        Layout3.GetComponent<Image>().color = Color.green;
    }

    public void MoveGuiElement(Vector2 position)
    {
        layoutsRect.anchoredPosition = position;
    }
}
