using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{

   public GameObject linePrefab;

   // int size=50;
    public int sizeX =50;
    public int sizeY =50;
    public int sizeZ = 50;
    public int nbPoints = 50;
    public float step = 0.1f;
    public float threshold = 0.1f;

    List<LineController> lineControllers = new List<LineController>();
    private void Start()
    {
        CreateGrid();
    }

    public void UpdateGrid()
    {
        foreach(LineController lineController in lineControllers)
        {
            //lineController.SetLineParameter(threshold, step, nbPoints);
            GameObject.Destroy(lineController.gameObject);
            //lineController.CreateLine();
        }
        CreateGrid();
    }
    void CreateGrid()
    {
        lineControllers.Clear();
        for (int k=0;k< sizeY; k++ )
        {
            for (int i = 0; i < sizeX; i++)
            {
                GameObject prefab = Instantiate(linePrefab);
                prefab.GetComponent<LineController>().SetLineParameter(threshold, sizeX*step, nbPoints,false);
                prefab.transform.position = new Vector3(i * step, k* step, 0);
                lineControllers.Add(prefab.GetComponent<LineController>());
                prefab.transform.parent=gameObject.transform;
            }

            for (int j = 0; j < sizeZ; j++)
            {
                GameObject prefab = Instantiate(linePrefab);
                prefab.GetComponent<LineController>().SetLineParameter(threshold, sizeZ * step, nbPoints, true);
                prefab.transform.position = new Vector3(0, k * step, j * step);
                lineControllers.Add(prefab.GetComponent<LineController>());
                prefab.transform.parent = gameObject.transform;
            }

        }
    }
}
