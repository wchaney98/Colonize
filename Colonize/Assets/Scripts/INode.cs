using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{
    bool Dead { get; set; }
    Vector3 Position { get; set; }
    bool ReceivingResources { get; set; }
    int Level { get; set; }
    int Life { get; set; }
    int MaxLife { get; set; }
    int DecaySpeed { get; set; }
    int VirusResistance { get; set; }
    float MoveSpeed { get; set; }
    List<INode> ConnectedNodes { get; set; }
    NodeMenu NodeMenu { get; set; }

    void ConnectTo(INode otherNode);
    void MoveTo(Vector2 mousePos);
    void AddConnectedNode(INode otherNode);
    void DisconnectFrom(INode otherNode);
    void RemoveConnectedNode(INode otherNode);
    void ReceiveResources(int amount, INode sender, INode originalSender);
    void DestroySelf();
}
