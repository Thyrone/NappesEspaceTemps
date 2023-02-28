using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public float threshold=0.1f;
    public float lenght=0.1f;
    public int pointNbr=50;


    LineRenderer lineRenderer;
    GameObject followObj;
    List<GameObject> followObjs=new List<GameObject>();
    //GameObject[] followObjs;
    public bool horizontal;

    private float waitTime = 0.05f;
    private float timer = 0.0f;
    Vector3[] initialLine;
    List<Vector3[]> modifLine = new List<Vector3[]>();

    void OnEnable()
    {
        MultipleTouch.OnPlanetCreated += AddPlanet;
        MultipleTouch.OnPlanetDestroy += DeletePlanet;
        //LevelManager.OnLevelChanged += UpdatePlanet;
    }


    void OnDisable()
    {
        MultipleTouch.OnPlanetCreated -= AddPlanet;
        MultipleTouch.OnPlanetDestroy -= DeletePlanet;
        // LevelManager.OnLevelChanged -= AddPlanet;
    }

    public void SetLineParameter(float _treshold,float _lenght,int _pointNbr,bool _horizontal)
    {
        threshold = _treshold;
        lenght = _lenght;
        pointNbr = _pointNbr;
        horizontal = _horizontal;
    }

    public void SetLineParameter(float _treshold, float _lenght, int _pointNbr)
    {
        threshold = _treshold;
        lenght = _lenght;
        pointNbr = _pointNbr;
    }

    private void Awake()
    {
        GameObject[] bodiesTab;
        bodiesTab = GameObject.FindGameObjectsWithTag("Planet");
        foreach (GameObject celestial in bodiesTab)
        {
            followObjs.Add(celestial);
        }
    }
    void Start()
    {
     //   UpdatePlanet();
        lineRenderer = GetComponent<LineRenderer>();
        CreateLine();
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        // Check if we have reached beyond 2 seconds.
        // Subtracting two is more accurate over time than resetting to zero.
        if (timer > waitTime)
        {
            UpdateLine();
            // Remove the recorded 2 seconds.
            timer = timer - waitTime;
        }
        
    }

    public void CreateLine()
    {
        lineRenderer.positionCount = pointNbr;


        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            if (horizontal)
                lineRenderer.SetPosition(i, new Vector3(i * (lenght / pointNbr), transform.position.y, transform.position.z));
            else
                lineRenderer.SetPosition(i, new Vector3(transform.position.x, transform.position.y, i * (lenght/ pointNbr)));

        }
        initialLine = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(initialLine);
        Debug.Log(initialLine.Length);
        UpdateLine();
    }
    void UpdateLine()
    {
        /*
        //Single
        if (followObj != null)
        {
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                lineRenderer.SetPosition(i, Vector3.Lerp(initialLine[i], followObj.transform.position, Mathf.Clamp(threshold - Vector3.Distance(initialLine[i], followObj.transform.position), 0, 1)));
            }
        }
        */

        //Multiples
        Debug.Log("followObjs.Length="+ followObjs.Count);
        if (followObjs.Count > 0)
        {
            Debug.Log("notNull");
            modifLine.Clear();
            foreach (GameObject obj in followObjs)
            {
                if (obj != null)
                {
                    //float localthreshold = 0.6f;
                    float localthreshold = obj.GetComponent<Rigidbody>().mass / 1000;
                    Vector3[] tabToAdd = new Vector3[lineRenderer.positionCount];
                    for (int i = 0; i < lineRenderer.positionCount; i++)
                    {
                        tabToAdd[i] = Vector3.Lerp(initialLine[i], obj.transform.position,
                            Mathf.Clamp(localthreshold - Vector3.Distance(initialLine[i], obj.transform.position)
                            , 0, 1));
                        //lineRenderer.SetPosition(i,Vector3.Lerp(initialLine[i], obj.transform.position, Mathf.Clamp(localthreshold - Vector3.Distance(initialLine[i], obj.transform.position), 0, 1)));
                    }
                    modifLine.Add(tabToAdd);
                }

            }
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                lineRenderer.SetPosition(i, VectorAverage(i, modifLine));
            }
        }else
        {
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                lineRenderer.SetPosition(i, initialLine[i]);
            }
        }
        

        
    }


    Vector3 VectorAverage(int index,List<Vector3[]> listOfVector)
    {
        Vector3 average = Vector3.zero;
        int divider = listOfVector.Count;
        foreach (Vector3[] calculLine in listOfVector)
        {

            average += calculLine[index];
        }

        return new Vector3(average.x / listOfVector.Count,
            average.y / listOfVector.Count,
            average.z / listOfVector.Count);
        
    }
    void AddPlanet(GameObject Planet)
    {
        //Singles
       // followObj = GameObject.FindGameObjectWithTag("Planet");

        //Multiples
        followObjs.Add(Planet);
    }

    void DeletePlanet(GameObject Planet)
    {
        followObjs.Remove(Planet);
    }
}
