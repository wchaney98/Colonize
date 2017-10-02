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
    public List<INode> SelectedNodes { get; set; }
    public List<Virus> Viruses = new List<Virus>();

    public GameObject BasicNodePrefab;
    public GameObject AqueductNodePrefab;
    public GameObject VirusPrefab;
    public GameObject HelpPanel;

    public NodeManager NodeManager;
    private Camera cam;

    private float virusTimer = 0;
    private float virusSpawnRate = 5f;

    private bool paused = false;

    private bool slowedDown = false;
    private float slowedDownTimePassed = 0f;

    private float tenTimesAbilityTime = 0f;

    public void ProcessSelection(Vector2 point1, Vector2 point2)
    {
        SelectedNodes.Clear();
        Rect bounds = new Rect(point1.x, point1.y, point2.x - point1.x, point2.y - point1.y);

        foreach (INode node in NodeManager.Nodes)
        {
            if (bounds.Contains(cam.WorldToScreenPoint(node.Position), true))
            {
                SelectedNodes.Add(node);
                node.NodeMenu.ActivateForNode(node);
            }
        }
        
    }

    public void SlowdownTime()
    {
        slowedDown = true;
    }

    public void GiveLifeToAll(int amount)
    {
        foreach (INode node in NodeManager.Nodes)
        {
            node.ReceiveResources(amount, null, null);
        }
    }

    public void ResetViruses()
    {
        foreach (Virus v in Viruses)
        {
            Destroy(v.gameObject);
        }
        Viruses.Clear();
    }

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
        SelectedNodes = new List<INode>();

        Persistence.existing.Time = 0;
        Persistence.existing.TenTimesAbilityActive = false;
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
        if (slowedDown && slowedDownTimePassed < Constants.ABILITY_SLOWDOWN_DURATION)
        {
            Time.timeScale = Constants.ABILITY_SLOWDOWN_TIMESCALE;
            slowedDownTimePassed += Time.deltaTime;
        }
        else
        {
            if (!paused) Time.timeScale = 1f;
            slowedDownTimePassed = 0f;
            slowedDown = false;
        }

        if (Persistence.existing.TenTimesAbilityActive && tenTimesAbilityTime < Constants.ABILITY_TEN_TIMES_DURATION)
        {
            tenTimesAbilityTime += Time.deltaTime;
        }
        else
        {
            tenTimesAbilityTime = 0f;
            Persistence.existing.TenTimesAbilityActive = false;
        }


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
            if (PlayerState == PlayerState.FREE && SelectedNodes != null)
            {
                Debug.Log("Attempting move (RIGHT CLICK)");
                foreach (INode node in SelectedNodes)
                {
                    node.MoveTo(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                } 
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (PlayerState == PlayerState.MOVING)
            {
                Debug.Log("Attempting move (LEFT CLICK)");
                foreach (INode node in SelectedNodes)
                {
                    node.MoveTo(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
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
}
