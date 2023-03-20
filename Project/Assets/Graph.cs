using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pathfinder;

public class Graph
{
    List<Connection> mConnections;

    // an array of connections outgoing from the given node
    public List<Connection> getConnections(Node fromNode)
    {
        List<Connection> connections = new List<Connection>();
        foreach (Connection c in mConnections)
        {
            if (c.getFromNode() == fromNode)
            {
                connections.Add(c);
            }
        }
        return connections;
    }

    public void Build(PathFindingType pathFindingType)
    {
        // find all nodes in scene
        // iterate over the nodes
        //   create connection objects,
        //   stuff them in mConnections
        
        mConnections = new List<Connection>();

        Node[] nodes = GameObject.FindObjectsOfType<Node>();
        
        if (pathFindingType == PathFindingType.DistanceBased)
        {
            foreach (Node fromNode in nodes)
            {
                foreach (Node toNode in fromNode.ConnectsTo)
                {
                    float cost = (toNode.transform.position - fromNode.transform.position).magnitude;
                    Debug.Log(cost);
                    Connection c = new Connection(cost, fromNode, toNode);
                    mConnections.Add(c);
                }
            }
        }
        else if (pathFindingType == PathFindingType.DangerLevel)
        {
            foreach (Node fromNode in nodes)
            {
                foreach (Node toNode in fromNode.ConnectsTo)
                {
                    // calculate cost based on danger level
                    float dangerLevel = (/*fromNode.DangerLevel*/ toNode.DangerLevel) * 20f;
                    float cost = /*(toNode.transform.position - fromNode.transform.position).magnitude + */dangerLevel;
                    Debug.Log(cost);

                    Connection c = new Connection(cost, fromNode, toNode);
                    mConnections.Add(c);
                }
            }
        }
    }
}

public class Connection
{
    float cost;
    Node fromNode;
    Node toNode;

    public Connection(float cost, Node fromNode, Node toNode)
    {
        this.cost = cost;
        this.fromNode = fromNode;
        this.toNode = toNode;
    }
    public float getCost()
    {
        return cost;
    }

    public Node getFromNode()
    {
        return fromNode;
    }

    public Node getToNode()
    {
        return toNode;
    }
}
