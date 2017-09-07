using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNode : MonoBehaviour, INode
{
    public Vector3 Position { get; set; }
    public int Level { get; set; }

    public int Life { get; set; }

    public int MaxLife { get; set; }

    public int DecaySpeed { get; set; }
    private int DecayCounter = 0;
    private int FramesPerDecay = 60;

    public List<INode> ConnectedNodes { get; set; }

    private TextMesh textMesh;

    void OnMouseDown()
    {
        Debug.Log("click");
    }

    public void ConnectTo(INode otherNode)
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
        }
    }

    public void AddConnectedNode (INode otherNode)
    {
        if (!ConnectedNodes.Contains(otherNode))
        {
            ConnectedNodes.Add(otherNode);
        }
    }

    public void DisconnectFrom (INode otherNode)
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

    public void RemoveConnectedNode (INode otherNode)
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

    void Start ()
    {
        Position = transform.position;
        textMesh = transform.GetChild(0).GetComponent<TextMesh>();
        Level = 1;
        Life = 100;
        MaxLife = 100;
        DecaySpeed = 1;
	}
	
	void Update ()
    {
        textMesh.text = Mathf.Floor((((float)Life / MaxLife) * 100)).ToString() + "%";
        DecayCounter++;
        if (DecayCounter >= FramesPerDecay * (ConnectedNodes.Count + 1))
        {
            Life -= DecaySpeed;
            DecayCounter = 0;
        }
            
        if (Life <= 0)
        {
            DestroyObject(gameObject);
        }
	}
}
