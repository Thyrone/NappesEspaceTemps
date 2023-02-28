using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prebab;
    public float initialTimeStep = 5;
    float timestep;

    NBodySimulation nBody;

    // Start is called before the first frame update
    void Start()
    {
        timestep = initialTimeStep;
        nBody = FindObjectOfType<NBodySimulation>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (timestep > 0)
        {
            timestep -= Time.deltaTime;
        }
        else
        {
            timestep = initialTimeStep;
            GameObject go =Instantiate(prebab);
            go.transform.position = transform.position;
            nBody.AddBodie(go.GetComponent<CelestialBody>());
            Debug.Log("Time has run out!");
            
        }
    }
}
