using System;
using UnityEngine;

[Serializable]
public class Node
{
    public Vector2 position;
    public bool walkable;
    public Node parent;

    public Node(Vector2 pos, bool walk)
    {
        position = pos;
        walkable = walk;
    }
}

public class SearchNode
{
    public Node baseNode;
    public float gCost = Mathf.Infinity;
    public float hCost;
    public float fCost => gCost + hCost;

    public SearchNode parent;

    public SearchNode(Node n)
    {
        baseNode = n;
    }
}
