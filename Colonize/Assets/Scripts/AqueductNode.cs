using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum ResourceType
{
    NORMAL,
    CONCENTRATED,
    PURE
}

public class AqueductNode : MonoBehaviour, INode
{
    public string NodeInfo
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Node Type: " + GetType().Name).
                AppendLine("Life: " + Life + "/" + MaxLife).
                AppendLine("Level: " + Level);
            return sb.ToString();
        }
    }

    public bool Dead { get; set; }

    public Vector3 Position { get; set; }

    public bool ReceivingResources { get; set; }

    public int Level { get; set; }

    public int Life { get; set; }

    public int MaxLife { get; set; }

    public GameObject ResourcesReceivedEffect;

    public int DecaySpeed { get; set; }
    private float DecayCounter = 0;
    private float SecPerDecay = 0.7f;
    private int MoveSpeed = 3;

    private float GatherCounter = 0;
    private float SecPerGather = 1.2f;
    private bool gathering = false;
    private static readonly int LIFE_PER_GATHER = 7;

    private int receivedResourcesLightupFrames = 0;
    private int lightupFrames = 7;

    public List<INode> ConnectedNodes { get; set; }

    private TextMesh textMesh;
    private SpriteRenderer spriteRenderer;
    private NodeMenu nodeMenu;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Resource" && other.gameObject.GetComponent<Resource>().Gatherer == null)
        {
            gathering = true;
            GatherAndTransferResources(LIFE_PER_GATHER);
            other.gameObject.GetComponent<Resource>().Gatherer = this;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        GatherCounter += Time.deltaTime;
        if (GatherCounter >= SecPerGather)
        {
            if (other.tag == "Resource" && other.gameObject.GetComponent<Resource>().Gatherer as Object == this)
            {
                GatherAndTransferResources(LIFE_PER_GATHER);
            }
            GatherCounter = 0;
        }
        else if (other.tag == "Virus")
        {
            DecayCounter += Time.deltaTime * 4;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Resource")
        {
            gathering = false;
            GatherAndTransferResources(LIFE_PER_GATHER);
            other.gameObject.GetComponent<Resource>().Gatherer = null;
        }
    }

    void GatherAndTransferResources(int amount)
    {
        foreach (INode node in ConnectedNodes)
        {
            node.ReceiveResources(amount, this, this);
        }
    }

    void OnMouseDown()
    {
        if (nodeMenu.GameManager.PlayerState == PlayerState.FREE)
        {
            nodeMenu.GameManager.SelectedNode = this;
            nodeMenu.ActivateForNode(this);
        }
        else if (nodeMenu.GameManager.PlayerState == PlayerState.CONNECTING)
        {
            if (nodeMenu.GameManager.SelectedNode as Object != this)
            {
                nodeMenu.GameManager.SelectedNode.ConnectTo(this);
                nodeMenu.GameManager.PlayerState = PlayerState.FREE;
                nodeMenu.GameManager.SelectedNode = null;
                nodeMenu.DeActivate();
            }
            else
            {
                nodeMenu.GameManager.SelectedNode = null;
                nodeMenu.GameManager.PlayerState = PlayerState.FREE;
                nodeMenu.DeActivate();
            }
        }
    }

    private void OnMouseOver()
    {
        spriteRenderer.color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }

    public void ConnectTo(INode otherNode)
    {
        if (ConnectedNodes.Count > 0)
        {
            ConnectedNodes[0].ConnectedNodes.Remove(this);
        }
        ConnectedNodes.Clear();
        if (!ConnectedNodes.Contains(otherNode))
        {
            if (otherNode is BasicNode)
            {
                ConnectedNodes.Add(otherNode);
                otherNode.AddConnectedNode(this);
                Debug.Log("BASICNODE ConnectTo Result:" + ConnectedNodes);
                foreach (INode node in ConnectedNodes)
                {
                    Debug.Log("ConnectTo: " + node);
                }
            }
            if (otherNode is AqueductNode)
            {
                if (otherNode.ConnectedNodes.Count == 0)
                {
                    ConnectedNodes.Add(otherNode);
                    otherNode.AddConnectedNode(this);
                    Debug.Log("AQUEDUCTNODE ConnectTo Result:" + ConnectedNodes);
                    foreach (INode node in ConnectedNodes)
                    {
                        Debug.Log("ConnectTo: " + node);
                    }
                }
            }
        }
    }

    public void MoveTo(Vector2 mousePos)
    {
        StopAllCoroutines();
        Vector2 dir = mousePos - (Vector2)Position;
        dir.Normalize();
        StartCoroutine(MoveToPosition(mousePos, dir));
    }

    public void AddConnectedNode(INode otherNode)
    {
        if (!ConnectedNodes.Contains(otherNode))
        {
            ConnectedNodes.Add(otherNode);
        }
    }

    public void DisconnectFrom(INode otherNode)
    {
        if (ConnectedNodes.Contains(otherNode))
        {
            ConnectedNodes.Remove(otherNode);
            otherNode.RemoveConnectedNode(this);
            Debug.Log("DisconnectFrom Result:" + ConnectedNodes);
            foreach (INode node in ConnectedNodes)
            {
                Debug.Log("DisconnectFrom: " + node);
            }
        }
    }

    public void RemoveConnectedNode(INode otherNode)
    {
        if (ConnectedNodes.Contains(otherNode))
        {
            ConnectedNodes.Remove(otherNode);
        }
    }

    private void Awake()
    {
        ConnectedNodes = new List<INode>();
    }

    IEnumerator MoveToPosition(Vector2 newPos, Vector2 dir)
    {
        while (Vector2.Distance(transform.position, newPos) > 0.1f)
        {
            Vector2 temp = transform.position;
            temp += dir * MoveSpeed * Time.deltaTime;
            transform.position = temp;
            Position = transform.position;
            DecayCounter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    void Start()
    {
        Dead = false;
        Position = transform.position;
        textMesh = transform.GetChild(0).GetComponent<TextMesh>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ReceivingResources = false;
        Level = 1;
        Life = 100;
        MaxLife = 100;
        DecaySpeed = 1;
        nodeMenu = Resources.FindObjectsOfTypeAll<NodeMenu>()[0];
    }

    void Update()
    {
        textMesh.text = Mathf.Floor((((float)Life / MaxLife) * 100)).ToString() + "%";
        DecayCounter += Time.deltaTime;
        if (DecayCounter >= SecPerDecay * (ConnectedNodes.Count + 1))
        {
            Life -= DecaySpeed;
            DecayCounter = 0;
        }

        if (ReceivingResources)
        {
            if (receivedResourcesLightupFrames == 0)
            {
                GameObject go = Instantiate(ResourcesReceivedEffect, new Vector3(transform.position.x, transform.position.y, -5f), Quaternion.identity);
                Destroy(go, 2f);
            }
            receivedResourcesLightupFrames++;      
            if (receivedResourcesLightupFrames > lightupFrames)
            {
                ReceivingResources = false;
                receivedResourcesLightupFrames = 0;
            }
        }

        if (Life <= 0)
        {
            DestroySelf();
        }

        if (nodeMenu.GameManager.SelectedNode as Object == this)
        {
            spriteRenderer.color = new Color(1f, 1f, 0.2f, 1f);
        }
        else if (gathering)
        {
            spriteRenderer.color = new Color(0.2f, 1f, 1f, 1f);
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    public void ReceiveResources(int amount, INode sender, INode originalSender)
    {
        Life += amount;
        ReceivingResources = true;
        // Maybe add receive/transfer animation here?
        
        // Special Aqueduct behavior
        foreach (INode node in ConnectedNodes)
        {
            if (node != sender && node != originalSender)
            {
                node.ReceiveResources(amount, this, originalSender);
            }
        }
    }

    public void DestroySelf()
    {
        foreach (INode node in ConnectedNodes)
        {
            node.ConnectedNodes.Remove(this);
        }
        Destroy(gameObject);
        Dead = true;
        Destroy(this);
    }
}
