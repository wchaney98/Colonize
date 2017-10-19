using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCursor : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        Vector2 originalPos = transform.position;
        Vector2 newPos = originalPos + Persistence.Instance.GetMainStickData() * Time.deltaTime * 10;
        transform.position = newPos;
    }
}
