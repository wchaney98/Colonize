using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonFeedbackGrow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        SoundManager.Instance.DoPlayOneShot(SoundFile.ButtonClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        iTween.ScaleTo(gameObject, new Vector3(1.1f, 1.1f, transform.localScale.z), 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        iTween.ScaleTo(gameObject, new Vector3(1.0f, 1.0f, transform.localScale.z), 0.1f);
    }
}
