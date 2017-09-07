using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMenu : MonoBehaviour
{
    public void ActivateForNode(INode node)
    {
        gameObject.SetActive(true);
        transform.position = node.Position;

    }

	void Start ()
    {
        gameObject.SetActive(false);
	}
	
	void Update ()
    {
		
	}

}
