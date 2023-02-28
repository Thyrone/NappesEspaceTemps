using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{

    NBodySimulation nBody;
    private void Start()
    {
        nBody = FindObjectOfType<NBodySimulation>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Projectile")
        {
            LevelManager.instance.ChangeLevel();
        }
        collision.gameObject.SetActive(false);
        //collision.gameObject.GetComponent<CelestialBody>().enabled=false;
        //collision.gameObject.GetComponent<MeshRenderer>().enabled=false;
        //collision.gameObject.GetComponent<Collider>().enabled=false;
        nBody.UpdateBodies(FindObjectsOfType<CelestialBody>());
        Destroy(collision.gameObject);
        
    }
}
