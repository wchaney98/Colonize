using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{
    Vector3 Position { get; set; }
    int Level { get; set; }
    int Life { get; set; }
    int MaxLife { get; set; }
    int DecaySpeed { get; set; }
    List<INode> ConnectedNodes { get; set; }

    void ConnectTo(INode otherNode);
    void MoveTo(Vector2 mousePos);
    void AddConnectedNode(INode otherNode);
    void DisconnectFrom(INode otherNode);
    void RemoveConnectedNode(INode otherNode);

}
