using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1.0f;

    Transform selectedUnit;
    bool unitSelected = false;
    public GameObject tilePosition;
    public GameObject cursor;
    public Camera cam;
    public GameObject camara;
    public GameObject stats;
    public GameObject turnos;
    public GameObject slide;
    public Transform Pos1;
    public Transform Pos2;
    public Transform Pos3;
    public Transform Pos4;

    List<Node> path = new List<Node>();


    GridManager gridManager;
    PathFinding pathFinder;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        tilePosition.SetActive(false);
        camara.transform.position = Pos1.position;
        camara.transform.rotation = Pos1.rotation;
    }

    void Update()
    { 
        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(mousePos);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);

        if (Input.GetKeyDown(KeyCode.Y)) 
        
        {
            slide.SetActive(false);
            turnos.SetActive(true);

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GirarAdelante();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GirarAtras();
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (hasHit)
            {
                if (hit.transform.tag == "Tile")
                {
                    if(unitSelected)
                    {
                        Vector3 targetCoords = hit.transform.GetComponent<Tiles>().coords;
                        Vector3 startCoords = new Vector3(selectedUnit.position.x, selectedUnit.position.y, selectedUnit.position.z) / gridManager.UnityGridSize;
                        pathFinder.SetNewDestination(startCoords, targetCoords);
                        RecalculatePath(true);
                    }
                }
                if (hit.transform.tag == "Unit")
                {
                    selectedUnit = hit.transform;
                    unitSelected = true;
                    tilePosition.SetActive(true);
                    cursor.SetActive(true);
                    stats.SetActive(true);
                }
            }
        }
    }

    void RecalculatePath(bool resetPath)
    {
        Vector3 coordinates = new Vector3();

        if(resetPath)
        {
            coordinates = pathFinder.StartCoords;
        }

        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath() 
    { 
        for(int i=1; i<path.Count; i++)
        {
            Vector3 startPosition = selectedUnit.position;
            Vector3 endPosition = gridManager.GetCoordinatesFromPosition(path[i].coords);
            float travelPercent = 0f;

            selectedUnit.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * movementSpeed;
                selectedUnit.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void GirarAdelante()
    {
        if (camara.transform.position == Pos1.position)
        {
            camara.transform.position = Pos2.position;
            camara.transform.rotation = Pos2.rotation;
        }
        if (camara.transform.position == Pos2.position)
        {
            camara.transform.position = Pos3.position;
            camara.transform.rotation = Pos3.rotation;
        }
        if (camara.transform.position == Pos3.position)
        {
            camara.transform.position = Pos4.position;
            camara.transform.rotation = Pos4.rotation;
        }
        if (camara.transform.position == Pos4.position)
        {
            camara.transform.position = Pos1.position;
            camara.transform.rotation = Pos1.rotation;
        }
    }

    public void GirarAtras()
    {
        if (camara.transform.position == Pos1.position)
        {
            camara.transform.position = Pos4.position;
            camara.transform.rotation = Pos4.rotation;
        }
        if (camara.transform.position == Pos2.position)
        {
            camara.transform.position = Pos1.position;
            camara.transform.rotation = Pos1.rotation; 
        }
        if (camara.transform.position == Pos3.position)
        {
            camara.transform.position = Pos2.position;
            camara.transform.rotation = Pos2.rotation;
        }
        if (camara.transform.position == Pos4.position)
        {
            camara.transform.position = Pos3.position;
            camara.transform.rotation = Pos3.rotation;
        }
    }

}
