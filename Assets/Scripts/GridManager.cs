using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector3 gridSize;
    [SerializeField] float unityGridSize;
    public float UnityGridSize { get { return unityGridSize; } }

    public Dictionary<Vector3, Node> grid = new Dictionary<Vector3, Node>();
    public Dictionary<Vector3, Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector3 coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }
        return null;
    }

    public void BlockNode(Vector3 coordinates)
    {
        if (grid.ContainsKey(coordinates)) 
        {
            grid[coordinates].walkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector3, Node> entry in grid) 
        {
            entry.Value.connectTo = null;
            entry.Value.explored = false;
            entry.Value.path = false;

        }
    }

    public Vector3 GetCoordinatesFromPosition(Vector3 position)
    {
        Vector3 coordinates = new Vector3();

        coordinates.x = position.x / unityGridSize;
        coordinates.y = position.z / unityGridSize;
        coordinates.z = position.x / unityGridSize;

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector3 coordinates)
    {
        Vector3 position = new Vector3();

        position.x = coordinates.x * unityGridSize;
        position.y = coordinates.z * unityGridSize;
        position.z = coordinates.x * unityGridSize;

        return position;
    }

    public void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    Vector3 coords = new Vector3(x, y, z);
                    grid.Add(coords, new Node(coords, true));

                    

                    //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //Vector3 position = new Vector3(coords.x *unityGridSize, coords.y * unityGridSize, coords.z *unityGridSize);
                    //cube.transform.position = position;
                    //cube.transform.SetParent(transform);

                }
            }
        }
    }
}
