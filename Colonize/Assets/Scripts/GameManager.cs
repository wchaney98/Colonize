using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    FREE,
    CONNECTING
}

public class GameManager : MonoBehaviour 
{
    public Material lineMat;

    public PlayerState PlayerState { get; set; }
    public INode SelectedNode { get; set; }

    private GameObject basicNodePrefab;

    private NodeManager nodeManager;
    private LineRenderer lineRenderer;

	void Start () 
	{
        nodeManager = new NodeManager(this);
        PlayerState = PlayerState.FREE;

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
	}
	
	void Update () 
	{

	}

    private void OnPostRender()
    {
        GL.PushMatrix();
        lineMat.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.LINES);
        GL.Color(Color.white);
        foreach (INode baseNode in nodeManager.Nodes)
        {
            Vector3 baseNodePos = Camera.current.WorldToScreenPoint(baseNode.Position);
            foreach (INode connectedNode in baseNode.ConnectedNodes)
            {
                Vector3 connectedNodePos = Camera.current.WorldToScreenPoint(connectedNode.Position);

                GL.Vertex(new Vector3(baseNodePos.x / Screen.width, baseNodePos.y / Screen.height));
                GL.Vertex(new Vector3(connectedNodePos.x / Screen.width, connectedNodePos.y / Screen.height));
            }
        }
        GL.End();
        GL.PopMatrix();
    }
}
