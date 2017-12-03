using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class AchievementFlare : MonoBehaviour
{
    float elapsedTime = 0f;
    Color textColor;

    public void Init(string s)
    {
        GetComponentInChildren<Text>().text = s;
    }
    private void Start()
    {
        gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
        Destroy(gameObject, 4f);
        textColor = GetComponentInChildren<Text>().color;
        //StartCoroutine(AlphaFade());
    }

    private IEnumerator AlphaFade()
    {
        while (true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 3f)
            {
                float incr = elapsedTime / 1;
                GetComponentInChildren<Text>().color = new Color(textColor.r, textColor.g, textColor.b, textColor.a - incr);
                yield return null;
            }
        }
    }

    private void Update()
    {
        
    }

}
