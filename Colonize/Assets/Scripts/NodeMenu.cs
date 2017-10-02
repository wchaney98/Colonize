using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMenu : MonoBehaviour
{
    public GameObject Camera;
    public GameManager GameManager;

    private INode attachedNode = null;

    public void SwitchToConnectState()
    {
        GameManager.PlayerState = PlayerState.CONNECTING;
        DeActivate();
    }

    public void SwitchToUpgradeState()
    {
        Debug.Log("Upgrade");
    }

    public void SwitchToMoveState()
    {
        GameManager.PlayerState = PlayerState.MOVING;
        DeActivate();
    }

    public void ActivateForNode(INode node)
    {
        attachedNode = node;
        gameObject.SetActive(true);      
    }

    public void DeActivate()
    {
        gameObject.SetActive(false);
    }

	void Start ()
    {
        GameManager = Camera.GetComponent<GameManager>();
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(Persistence.existing.Controls["ConnectKey"]))
        {
            SwitchToConnectState();
        }
    }

}
