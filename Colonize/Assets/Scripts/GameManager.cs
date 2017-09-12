using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    FREE,
    CONNECTING,
    MOVING,
    BUILDING_BASIC,
    BUILDING_AQUEDUCT
}

public class GameManager : MonoBehaviour 
{
    public Material lineMat;

    public PlayerState PlayerState { get; set; }
    public INode SelectedNode { get; set; }

    public GameObject BasicNodePrefab;
    public GameObject AqueductNodePrefab;

    private NodeManager nodeManager;
    private LineRenderer lineRenderer;

    public void ResetNodes()
    {
        nodeManager.RemoveAllNodes();
    }

	void Start () 
	{
        nodeManager = new NodeManager(this);
        PlayerState = PlayerState.FREE;
	}
	
	void Update () 
	{
        if (PlayerState == PlayerState.MOVING && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attempting move");
            SelectedNode.MoveTo(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            PlayerState = PlayerState.FREE;
        }
        if (PlayerState == PlayerState.BUILDING_BASIC && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject go = Instantiate(BasicNodePrefab, new Vector3(mousePos.x, mousePos.y), Quaternion.identity);
            BasicNode node = go.GetComponent<BasicNode>();
            nodeManager.AddNode(node);
            PlayerState = PlayerState.FREE;
            Debug.Log("Attempting build BASIC");
        }
        if (PlayerState == PlayerState.BUILDING_AQUEDUCT && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject go = Instantiate(AqueductNodePrefab, new Vector3(mousePos.x, mousePos.y), Quaternion.identity);
            AqueductNode node = go.GetComponent<AqueductNode>();
            nodeManager.AddNode(node);
            PlayerState = PlayerState.FREE;
            Debug.Log("Attempting build AQUEDUCT");
        }
    }

    private void OnPostRender()
    {
        GL.PushMatrix();
        lineMat.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.LINES);
        foreach (INode baseNode in nodeManager.Nodes)
        {
            Vector3 baseNodePos = Camera.current.WorldToScreenPoint(baseNode.Position);
            foreach (INode connectedNode in baseNode.ConnectedNodes)
            {
                Vector3 connectedNodePos = Camera.current.WorldToScreenPoint(connectedNode.Position);

                GL.Color(connectedNode.ReceivingResources ? Color.yellow : Color.white);
                GL.Vertex(new Vector3(baseNodePos.x / Screen.width, baseNodePos.y / Screen.height));
                GL.Vertex(new Vector3(connectedNodePos.x / Screen.width, connectedNodePos.y / Screen.height));
            }
        }
        GL.End();
        GL.PopMatrix();
    }
}
