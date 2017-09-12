using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMenu : MonoBehaviour
{
    public GameObject Camera;
    public GameManager GameManager;

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
        gameObject.SetActive(true);
        transform.position = node.Position;
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
		
	}

}
