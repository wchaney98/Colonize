﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerBackButton : MonoBehaviour
{
    void Update()
    {
        if (Persistence.Instance.ControllerIsConnected && Input.GetButtonDown("J_B"))
            SceneManager.LoadScene("Main Menu");
    }
}
