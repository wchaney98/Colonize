using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LeechNode : Node
{
    /*public string NodeInfo
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Node Type: " + GetType().Name).
                AppendLine("Life: " + Life + "/" + MaxLife).
                AppendLine("Level: " + Level);
            return sb.ToString();
        }
    }*/

    protected override float SecPerDecay { get; set; }
    public override float MoveSpeed { get; set; }

    private float GatherCounter = 0;
    private float SecPerGather = 1.2f;
    private float pulsatingGlowTimer = 0f;
    private bool gathering = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Virus")
        {
            gathering = true;
            GatherAndTransferResources(Constants.NORMAL_AQUEDUCT_LIFE_PER_GATHER);
            other.gameObject.GetComponent<Virus>().Leech(Constants.LEECH_NODE_RESOURCE_AMOUNT);
        }
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);

        GatherCounter += Time.deltaTime;
        if (GatherCounter >= SecPerGather)
        {
            if (other.tag == "Virus")
            {
                if (Persistence.Instance.TenTimesAbilityActive)
                {
                    other.gameObject.GetComponent<Virus>().Leech(Constants.LEECH_NODE_RESOURCE_AMOUNT);
                    GatherAndTransferResources(Constants.NORMAL_AQUEDUCT_LIFE_PER_GATHER * 10);
                }
                else
                {
                    other.gameObject.GetComponent<Virus>().Leech(Constants.LEECH_NODE_RESOURCE_AMOUNT);
                    GatherAndTransferResources(Constants.NORMAL_AQUEDUCT_LIFE_PER_GATHER);
                }
            }
            GatherCounter = 0;
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if (other.tag == "Virus")
        {
            gathering = false;
        }
    }

    private void GatherAndTransferResources(int amount)
    {
        foreach (INode node in ConnectedNodes)
        {
            node.ReceiveResources(amount, this, this);
        }
    }

    public override void ConnectTo(INode otherNode)
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
                // Remove all connections to this node... to and from... before "stealing" this connection
                if (otherNode.ConnectedNodes.Count != 0)
                {
                    otherNode.ConnectedNodes[0].ConnectedNodes.Remove(otherNode);
                    otherNode.ConnectedNodes.Clear();
                }
                ConnectedNodes.Add(otherNode);
                otherNode.AddConnectedNode(this);
                Debug.Log("AQUEDUCTNODE ConnectTo Result:" + ConnectedNodes);
                foreach (INode node in ConnectedNodes)
                {
                    Debug.Log("ConnectTo: " + node);
                }
            }
            if (otherNode is LeechNode)
            {
                // Remove all connections to this node... to and from... before "stealing" this connection
                if (otherNode.ConnectedNodes.Count != 0)
                {
                    otherNode.ConnectedNodes[0].ConnectedNodes.Remove(otherNode);
                    otherNode.ConnectedNodes.Clear();
                }
                ConnectedNodes.Add(otherNode);
                otherNode.AddConnectedNode(this);
                Debug.Log("LEECHNODE ConnectTo Result:" + ConnectedNodes);
                foreach (INode node in ConnectedNodes)
                {
                    Debug.Log("ConnectTo: " + node);
                }
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        SecPerDecay = 0.7f;
        MoveSpeed = 3;
    }

    protected override void Update()
    {
        collidingWithVirus = false;
        base.Update();
        if (gathering)
        {
            if (pulsatingGlowTimer == 0)
            {
                iTween.ColorUpdate(gameObject, new Color(0.2f, 1f, 1f, 0.8f), 0.5f);
            }
            if (pulsatingGlowTimer >= 0.5f)
            {
                iTween.ColorUpdate(gameObject, new Color(0.2f, 1f, 1f, 1f), 0.5f);
            }
            if (pulsatingGlowTimer >= 1f)
            {
                pulsatingGlowTimer = 0f;
            }
            pulsatingGlowTimer += Time.deltaTime;
        }
    }

    public override void ReceiveResources(int amount, INode sender, INode originalSender)
    {
        base.ReceiveResources(amount, sender, originalSender);

        // Special Aqueduct behavior
        foreach (INode node in ConnectedNodes)
        {
            if (node != sender && node != originalSender)
            {
                node.ReceiveResources(amount, this, originalSender);
            }
        }
    }
}
