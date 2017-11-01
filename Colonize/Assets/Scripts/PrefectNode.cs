using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class PrefectNode : Node
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

    private bool connectedToAnotherPrefect = false;

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
            if (otherNode is AqueductNode || otherNode is LeechNode || otherNode is PrefectNode)
            {
                // Remove all connections to this node... to and from... before "stealing" this connection
                if (otherNode.ConnectedNodes.Count != 0)
                {
                    otherNode.ConnectedNodes[0].ConnectedNodes.Remove(otherNode);
                    otherNode.ConnectedNodes.Clear();
                }
                ConnectedNodes.Add(otherNode);
                otherNode.AddConnectedNode(this);
                Debug.Log(otherNode.GetType() + " ConnectTo Result:" + ConnectedNodes);
                foreach (INode node in ConnectedNodes)
                {
                    Debug.Log("ConnectTo: " + node);
                }

                if (otherNode is PrefectNode)
                {
                    connectedToAnotherPrefect = true;
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
        base.Update();

        if (ConnectedNodes.Count == 0)
        {
            connectedToAnotherPrefect = false;
        }
    }

    private void FixedUpdate()
    {
        if (connectedToAnotherPrefect)
        {
            Vector2 dir = ConnectedNodes[0].Position - Position;
            float distance = Vector2.Distance(ConnectedNodes[0].Position, Position);
            RaycastHit2D[] hits = Physics2D.RaycastAll(Position, dir, distance);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.tag == "Virus")
                {
                    hit.collider.gameObject.GetComponent<Virus>().StuckThisFrame = true;
                }
            }
        }
    }

    public override void ReceiveResources(int amount, INode sender, INode originalSender)
    {
        base.ReceiveResources(amount, sender, originalSender);
    }
}
