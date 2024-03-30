using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteAlways]
public class Labeller : MonoBehaviour
{
    TextMeshPro Label;
    public Vector3 coords = new Vector3();
    GridManager gridManager;

    public void Awake()
    {
        gridManager = FindAnyObjectByType<GridManager>();
        Label = GetComponentInChildren<TextMeshPro>();

        DisplayCoords();
    }

    void Update()
    {
        DisplayCoords();
        transform.name = coords.ToString();
    }

    public void DisplayCoords()
    {
        if (!gridManager) { return; }

        coords.x = transform.position.x / gridManager.UnityGridSize;
        coords.y = transform.position.z / gridManager.UnityGridSize;
        coords.z = transform.position.y / gridManager.UnityGridSize;

        Label.text = $"{coords.x},{coords.z},{coords.y}";
    }
}
