using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    [SerializeField] Vector3 startCoords;

    public Vector3 StartCoords { get { return startCoords; } }

    [SerializeField] Vector3 targetCoords;

    public Vector3 TargetCoords { get { return targetCoords; } }

    Node startNode;
    Node targetNode;
    Node currentNode;

    Queue<Node> frontier = new Queue<Node>();

    Dictionary<Vector3, Node> reached = new Dictionary<Vector3, Node>();

    GridManager gridManager;
    Dictionary<Vector3, Node> grid = new Dictionary<Vector3, Node>();

    Vector3[] searchOrder = {Vector3.right, Vector3.left, Vector3.up, Vector3.down};


    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>(); 
        if (gridManager != null)
        {
            grid = gridManager.Grid;
        }
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(StartCoords);
    }

    public List<Node> GetNewPath(Vector3 coordinates)
    {
        gridManager.ResetNodes();

        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    void BreadthFirstSearch(Vector3 coordinates)
    {
        startNode.walkable = true;
        targetNode.walkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while (frontier.Count > 0 && isRunning == true)
        {
            currentNode = frontier.Dequeue();
            currentNode.explored = true;
            ExploreNeighbors();
            if (currentNode.coords == TargetCoords)
            {
                isRunning = false;
            }
        }
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector3 direction in searchOrder)
        {
            Vector3 neighborCoords = currentNode.coords + direction;

            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
        }

        foreach(Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coords) && neighbor.walkable)
            {
                neighbor.connectTo = currentNode;
                reached.Add(neighbor.coords, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        path.Add(currentNode);
        currentNode.path = true;

        while (currentNode.connectTo != null)
        {
            currentNode = currentNode.connectTo;
            path.Add(currentNode);
            currentNode.path = true;
        }

        path.Reverse();

        return path;
    }

    public void NotifyRecievers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

    public void SetNewDestination(Vector3 startCoordinates, Vector3 targetCoordinates)
    {
        startCoords = startCoordinates;
        targetCoords = targetCoordinates;
        startNode = grid[this.startCoords];
        targetNode = grid[this.targetCoords];
        GetNewPath();
    }

    public bool WillBlockPath(Vector3 coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].walkable;

            grid[coordinates].walkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].walkable = previousState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;

            }

        }
        return false;
    }

}
