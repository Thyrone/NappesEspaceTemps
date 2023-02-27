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
    public bool horizontal;

    private float waitTime = 0.05f;
    private float timer = 0.0f;
    Vector3[] initialLine;
    // Start is called before the first frame update
    void OnEnable()
    {
        MultipleTouch.OnPlanetCreated += UpdatePlanet;
    }


    void OnDisable()
    {
        MultipleTouch.OnPlanetCreated -= UpdatePlanet;
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
    void Start()
    {

        followObj = GameObject.FindGameObjectWithTag("Planet");
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
        if (followObj != null)
        {
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                lineRenderer.SetPosition(i, Vector3.Lerp(initialLine[i], followObj.transform.position, Mathf.Clamp(threshold - Vector3.Distance(initialLine[i], followObj.transform.position), 0, 1)));
            }
        }
    }

    void UpdatePlanet()
    {
        followObj = GameObject.FindGameObjectWithTag("Planet");
    }
}
