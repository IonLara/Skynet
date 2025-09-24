using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class Grid : MonoBehaviour
{
    public Dictionary<Vector2, Node> nodes = new Dictionary<Vector2, Node>();
    public int gridSize = 10;

    public LayerMask obstacleLayer;

    void Awake()
    {
        for (int i = -gridSize/2; i < gridSize/2; i++)
        {
            for (int j = -gridSize/2; j < gridSize/2; j++)
            {
                bool foo = Physics.OverlapBox(new Vector3(i, 0.5f, j), new Vector3(0.3f, 0.3f, 0.3f), Quaternion.identity, obstacleLayer).Length == 0;
                nodes.Add(new Vector2(i, j), new Node(new Vector2(i, j), foo));

                Debug.Log("Node at " + i + ", " + j + " is " + (foo ? "walkable" : "not walkable"));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
