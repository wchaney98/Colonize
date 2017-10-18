using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour, INode
{
    public bool Dead { get; set; }
    public Vector3 Position { get; set; }
    public bool ReceivingResources { get; set; }
    public int Level { get; set; }
    public int Life { get; set; }
    public int MaxLife { get; set; }
    public int DecaySpeed { get; set; }
    public int VirusResistance { get; set; }
    public List<INode> ConnectedNodes { get; set; }

    public GameObject ResourcesReceivedEffect;

    protected float DecayCounter = 0;
    protected abstract float SecPerDecay { get; set; }
    public abstract float MoveSpeed { get; set; }

    protected int receivedResourcesLightupFrames = 0;
    protected int lightupFrames = 7;

    protected TextMesh textMesh;
    protected SpriteRenderer spriteRenderer;
    public NodeMenu NodeMenu { get; set; }

    protected float takingDamageSoundTimer = 0f;

    private bool lowHealthSoundPlayed = false;

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        takingDamageSoundTimer += Time.deltaTime;
        if (other.tag == "Virus")
        {
            DecayCounter += Time.deltaTime * (Constants.VIRUS_DECAY_MULTIPLIER - VirusResistance);
            if (takingDamageSoundTimer >= 1f)
            {
                SoundManager.Instance.DoPlayOneShot(SoundFile.NodeTakingDamage, Position);
                takingDamageSoundTimer = 0f;
            }
        }
    }

    public virtual void OnMouseDown()
    {
        if (NodeMenu.GameManager.PlayerState == PlayerState.FREE)
        {
            NodeMenu.GameManager.SelectedNodes.Clear();
            NodeMenu.GameManager.SelectedNodes.Add(this);
            NodeMenu.ActivateForNode(this);
        }
        else if (NodeMenu.GameManager.PlayerState == PlayerState.CONNECTING)
        {
            foreach (INode node in NodeMenu.GameManager.SelectedNodes)
            {
                if (node as Object != this)
                {
                    node.ConnectTo(this);
                    SoundManager.Instance.DoPlayOneShot(SoundFile.DidConnect, Position);
                }
                else
                {
                    SoundManager.Instance.DoPlayOneShot(SoundFile.BadAction, Position);
                    break;
                }
            }
            NodeMenu.DeActivate();
            NodeMenu.GameManager.PlayerState = PlayerState.FREE;
            NodeMenu.GameManager.SelectedNodes.Clear();
        }
    }

    protected void OnMouseOver()
    {
        spriteRenderer.color = new Color(0.9f, 0.9f, 0.9f, 0.9f);
    }

    protected void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }

    public abstract void ConnectTo(INode otherNode);

    public void MoveTo(Vector2 mousePos)
    {
        if (gameObject != null)
        {
            StopAllCoroutines();
            Vector2 dir = mousePos - (Vector2)Position;
            dir.Normalize();
            StartCoroutine(MoveToPosition(mousePos, dir));
        }
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

    protected void Awake()
    {
        ConnectedNodes = new List<INode>();
    }

    protected IEnumerator MoveToPosition(Vector2 newPos, Vector2 dir)
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

    protected virtual void Start()
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
        VirusResistance = 0;
        NodeMenu = Resources.FindObjectsOfTypeAll<NodeMenu>()[0];
    }

    protected virtual void Update()
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
            SoundManager.Instance.DoPlayOneShot(SoundFile.NodeDied1, Position);
            DestroySelf();
        }
        if (Life == 20 && !lowHealthSoundPlayed)
        {
            SoundManager.Instance.DoPlayOneShot(SoundFile.LowHealthAlert, Position);
            lowHealthSoundPlayed = true;
        }
        if (Life <= 20)
        {
            textMesh.color = new Color(1f, 0.1f, 0.1f, 1f);
        }
        else
        {
            textMesh.color = new Color(0f, 0f, 0f, 1f);
        }

        if (NodeMenu.GameManager.SelectedNodes.Contains(this))
        {
            //spriteRenderer.color = new Color(0.9f, 0.9f, 0.2f, 1f);
            iTween.ColorUpdate(gameObject, new Color(0.9f, 0.9f, 0.2f, 0.95f), 1f);
        }

        else
        {
            iTween.ColorUpdate(gameObject, Color.white, 0.2f);
        }
    }

    public virtual void ReceiveResources(int amount, INode sender, INode originalSender)
    {
        Life += amount;
        ReceivingResources = true;
        if (sender == null && originalSender == null)
            return;
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
