using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BasicNode : Node
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

    public override void ConnectTo(INode otherNode)
    {
        if (!ConnectedNodes.Contains(otherNode))
        {
            if (otherNode is BasicNode)
            {
                ConnectedNodes.Add(otherNode);
                otherNode.AddConnectedNode(this);
                Debug.Log("ConnectTo Result:" + ConnectedNodes);
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

    protected override void Start()
    {
        base.Start();
        SecPerDecay = 0.6f;
        MoveSpeed = 2;
    }

    protected override void Update()
    {
        base.Update();
    }
}
