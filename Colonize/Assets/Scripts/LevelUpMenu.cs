using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpMenu : MonoBehaviour
{
    public GameObject Camera;
    public GameManager GameManager;

    public void LevelUp1()
    {

    }

    public void LevelUp2()
    {

    }

    public void LevelUp3()
    {

    }

    void Start()
    {
        GameManager = Camera.GetComponent<GameManager>();
    }

    private void Update()
    {
        // Check if level requirements are met
    }
}