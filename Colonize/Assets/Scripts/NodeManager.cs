using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager 
{
    private List<INode> Nodes { get; set; }

    public NodeManager()
    {
        Nodes = new List<INode>();
    }

    public void AddNode(INode node)
    {
        Nodes.Add(node);
    }

    public void DrawConnections(LineRenderer lr)
    {
        foreach (INode baseNode in Nodes)
        {
            foreach (INode connectedNode in baseNode.ConnectedNodes)
            {
                
            }
        }
    }
}
