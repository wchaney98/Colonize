using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public static GameObject current;

    public Material lineMat;

    public PlayerState PlayerState { get; set; }
    public INode SelectedNode { get; set; }

    public GameObject BasicNodePrefab;
    public GameObject AqueductNodePrefab;
    public GameObject VirusPrefab;
    public GameObject HelpPanel;

    public NodeManager NodeManager;
    private Camera cam;

    private float secondTimer = 0;
    private float virusTimer = 0;
    private float virusSpawnRate = 5f;

    private bool paused = false;

    public void ResetNodes()
    {
        NodeManager.RemoveAllNodes();
    }

    private void Awake()
    {
        NodeManager = new NodeManager(this);
    }

    void Start () 
	{
        current = gameObject;

        Persistence.existing.Time = 0;
        PlayerState = PlayerState.FREE;
        cam = GetComponent<Camera>();
        HelpPanel.SetActive(false);

        // FOR THIS VER ONLY
        CreateNode("B", -0.5f, -2.5f);
        CreateNode("B", 3.8f, -0.5f);
        CreateNode("B", 2.1f, 2.5f);
        CreateNode("A", 6.6f, -1.5f);
        CreateNode("A", -1.9f, 1.9f);
        CreateNode("A", -4.6f, -0.2f);
    }
	
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (paused)
            {
                Time.timeScale = 0;
                HelpPanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                HelpPanel.SetActive(false);
            }
        }

        Persistence.existing.Time += Time.deltaTime;
        Persistence.existing.Time = float.Parse(Persistence.existing.Time.ToString("0.00"));

        virusTimer += Time.deltaTime;
        if (virusTimer >= virusSpawnRate)
        {
            Instantiate(VirusPrefab, new Vector3
                (Random.Range(1, 3) == 1 ? Random.Range(-10f, -8f) : Random.Range(8f, 10f),
                Random.Range(1, 3) == 1 ? Random.Range(-7f, -4f) : Random.Range(4f, 7f),
                8f),
                Quaternion.identity);
            virusTimer = 0;
            virusSpawnRate += 2f;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (PlayerState == PlayerState.FREE && SelectedNode != null)
            {
                Debug.Log("Attempting move (RIGHT CLICK)");
                SelectedNode.MoveTo(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (PlayerState == PlayerState.MOVING)
            {
                Debug.Log("Attempting move (LEFT CLICK)");
                SelectedNode.MoveTo(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                PlayerState = PlayerState.FREE;
            }
            else if (PlayerState == PlayerState.BUILDING_BASIC)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject go = Instantiate(BasicNodePrefab, new Vector3(mousePos.x, mousePos.y), Quaternion.identity);
                BasicNode node = go.GetComponent<BasicNode>();
                NodeManager.AddNode(node);
                PlayerState = PlayerState.FREE;
                Debug.Log("Attempting build BASIC");
            }
            else if (PlayerState == PlayerState.BUILDING_AQUEDUCT)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject go = Instantiate(AqueductNodePrefab, new Vector3(mousePos.x, mousePos.y), Quaternion.identity);
                AqueductNode node = go.GetComponent<AqueductNode>();
                NodeManager.AddNode(node);
                PlayerState = PlayerState.FREE;
                Debug.Log("Attempting build AQUEDUCT");
            }
        }

        foreach (INode baseNode in NodeManager.Nodes)
        {
            Vector3 baseNodePos = cam.WorldToScreenPoint(baseNode.Position);
            foreach (INode connectedNode in baseNode.ConnectedNodes)
            {
                Vector3 connectedNodePos = cam.WorldToScreenPoint(connectedNode.Position);
                VectorLine temp = VectorLine.SetLine(
                    baseNode.ReceivingResources && baseNode as AqueductNode != null ? Color.yellow : Color.white, 
                    Time.deltaTime, 
                    new Vector2(baseNodePos.x + Random.Range(-1, 1), baseNodePos.y + Random.Range(-1, 1)), 
                    new Vector2(connectedNodePos.x + Random.Range(-1, 1), connectedNodePos.y + Random.Range(-1, 1)));
                temp.SetWidth(1f);
                temp.Draw();
            }
        }
        NodeManager.Nodes.RemoveAll(x => x.Dead);

        if (NodeManager.Nodes.Count == 0)
        {
            SceneManager.LoadScene("Game Finish");
        }
    }

    private void CreateNode(string nodeType, float x, float y)
    {
        GameObject go = Instantiate(nodeType == "B" ? BasicNodePrefab : AqueductNodePrefab, new Vector3(x, y, -5f), Quaternion.identity);
        INode i = go.GetComponent<INode>();
        NodeManager.AddNode(i);
    }

    private void OnPostRender()
    {
        /*
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
        */
    }
}
