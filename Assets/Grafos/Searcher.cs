using UnityEngine;
using System.Collections.Generic;

public class Searcher : MonoBehaviour
{
    public Dictionary<Vector2, SearchNode> searchNodes = new Dictionary<Vector2, SearchNode>();

    public Grid grid;

    private HashSet<SearchNode> closedSet = new HashSet<SearchNode>();
    private List<SearchNode> openSet = new List<SearchNode>();

    public Vector2 targetPosition;

    public List<Node> path = new List<Node>();

    private MaiActions actions;
    private int currentPathIndex = 0;

    private Vector2[] offsets = new Vector2[]
    {
        new Vector2(1, 0),
        new Vector2(-1, 0),
        new Vector2(0, 1),
        new Vector2(0, -1)
    };

    void Start()
    {
        var nodes = grid.nodes;
        foreach (var kvp in nodes)
        {
            searchNodes.Add(kvp.Key, new SearchNode(kvp.Value));
        }
        openSet.Add(searchNodes[new Vector2((int)transform.position.x, (int)transform.position.z)]);

        while (openSet.Count > 0)
        {
            SearchNode current = openSet[0];

            openSet.Remove(current);
            closedSet.Add(current);

            if (current.baseNode.position == targetPosition)
            {
                path = ReconstructPath(current);
                break;
            }
            Debug.Log("Current node: " + current.baseNode.position);

            foreach (Vector2 offset in offsets)
            {
                Vector2 neighborPos = current.baseNode.position + offset;
                if (searchNodes.TryGetValue(neighborPos, out SearchNode neighbor) && neighbor.baseNode.walkable && !closedSet.Contains(neighbor))
                {
                    float tentativeG = current.gCost + 1;
                    if (tentativeG < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = tentativeG;
                        neighbor.hCost = Vector2.Distance(neighbor.baseNode.position, targetPosition);
                        neighbor.parent = current;

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }
        }
        actions = new MaiActions();
        actions.Gameplay.Enable();
    }


    List<Node> ReconstructPath(SearchNode endNode)
    {
        List<Node> path = new List<Node>();
        SearchNode current = endNode;

        while (current != null)
        {
            path.Add(current.baseNode);
            current = current.parent; // usamos SearchNode.parentNode, no BaseNode
        }

        path.Reverse(); // para que vaya de inicio a objetivo
        return path;
    }

    void Update()
    {
        if (actions.Gameplay.Step.triggered)
        {
            currentPathIndex++;
            if (currentPathIndex < path.Count)
            {
                Vector3 nextPos = new Vector3(path[currentPathIndex].position.x, transform.position.y, path[currentPathIndex].position.y);
                transform.position = nextPos;
            }
        }
    }
}

