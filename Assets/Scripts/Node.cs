using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 coords;
    public bool walkable;
    public bool explored;
    public bool path;
    public Node connectTo;

    public Node(Vector3 coords, bool walkable)
    {
        this.coords = coords;
        this.walkable = walkable;
    }
}
