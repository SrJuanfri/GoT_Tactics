using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public Vector3 coords;

    GridManager gridManager;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        float x = transform.position.x;
        float z = transform.position.z;
        float y = transform.position.y;

        coords = new Vector3(x /gridManager.UnityGridSize, z /gridManager.UnityGridSize, y /gridManager.UnityGridSize);

    }
}
