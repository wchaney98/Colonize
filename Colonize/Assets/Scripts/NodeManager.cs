using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager 
{
    public List<INode> Nodes { get; set; }
    public GameManager gm { get; set; }

    public NodeManager(GameManager gm)
    {
        Nodes = new List<INode>();
        this.gm = gm;
    }

    public void AddNode(INode node)
    {
        Nodes.Add(node);
    }
}
