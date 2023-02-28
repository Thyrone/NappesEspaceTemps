using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    NBodySimulation nBody;
    private void Start()
    {
        nBody = FindObjectOfType<NBodySimulation>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Planet")
        {
            nBody.DeleteBodie(gameObject.GetComponent<CelestialBody>());
           // gameObject.transform.position = Vector3.zero;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().isKinematic =true;
            Debug.Log("time="+ gameObject.GetComponent<TrailRenderer>().time);
            Destroy(gameObject, gameObject.GetComponent<TrailRenderer>().time);
        }
    }
}
