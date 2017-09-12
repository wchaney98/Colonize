using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour 
{
    public GameObject Camera;
    public GameManager GameManager;

    public void SwitchToBuildBasicNodeState()
    {
        GameManager.PlayerState = PlayerState.BUILDING_BASIC;
        Debug.Log("Switched to BUILDING_BASIC");
    }

    public void SwitchToBuildAqueductNodeState()
    {
        GameManager.PlayerState = PlayerState.BUILDING_AQUEDUCT;
        Debug.Log("Switched to BUILDING_AQUEDUCT");
    }
   
    public void ResetNodes()
    {
        GameManager.ResetNodes();
    }

    public void DeActivate()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        GameManager = Camera.GetComponent<GameManager>();
    }

    void Update()
    {

    }
}
