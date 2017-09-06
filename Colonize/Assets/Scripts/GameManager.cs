using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    private GameObject basicNodePrefab;

    private NodeManager nodeManager;
    private LineRenderer lineRenderer;

	void Start () 
	{
        nodeManager = new NodeManager();
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        basicNodePrefab = Resources.Load("Prefabs/BasicNode") as GameObject;
        Debug.Log(basicNodePrefab);
        GameObject testBN0 = Instantiate(basicNodePrefab, new Vector3(-3f, 1f), Quaternion.identity);
        GameObject testBN1 = Instantiate(basicNodePrefab, new Vector3(3f, -1f), Quaternion.identity);
        GameObject testBN2 = Instantiate(basicNodePrefab, new Vector3(3f, 1f), Quaternion.identity);
        GameObject testBN3 = Instantiate(basicNodePrefab, new Vector3(-3f, -1f), Quaternion.identity);

        BasicNode testBN0class = testBN0.GetComponent<BasicNode>();
        BasicNode testBN1class = testBN1.GetComponent<BasicNode>();
        BasicNode testBN2class = testBN2.GetComponent<BasicNode>();
        BasicNode testBN3class = testBN3.GetComponent<BasicNode>();

        nodeManager.AddNode(testBN0class);
        nodeManager.AddNode(testBN1class);
        nodeManager.AddNode(testBN2class);
        nodeManager.AddNode(testBN3class);

        testBN0class.ConnectTo(testBN1class);
        testBN0class.ConnectTo(testBN2class);
        testBN0class.DisconnectFrom(testBN1class);
        testBN2class.ConnectTo(testBN1class);
        testBN2class.DisconnectFrom(testBN0class);

        nodeManager.DrawConnections(lineRenderer);
	}
	
	void Update () 
	{
		
	}
}
