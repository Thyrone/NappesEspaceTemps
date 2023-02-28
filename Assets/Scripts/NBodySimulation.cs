using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBodySimulation : MonoBehaviour {
    //CelestialBody[] bodies;
    List<CelestialBody> bodies=new List<CelestialBody>();
    static NBodySimulation instance;

    void Awake () {
        CelestialBody[] bodiesTab;
        bodiesTab = FindObjectsOfType<CelestialBody> ();
        foreach(CelestialBody celestial in bodiesTab)
        {
            bodies.Add(celestial);
        }
        Time.fixedDeltaTime = Universe.physicsTimeStep;
        Debug.Log ("Setting fixedDeltaTime to: " + Universe.physicsTimeStep);

    }

    void FixedUpdate () {
        if(bodies.Count>0)
        {
            for (int i = 0; i < bodies.Count; i++)
            {
                if (bodies[i].Position != null)
                {
                    Vector3 acceleration = CalculateAcceleration(bodies[i].Position, bodies[i]);
                    bodies[i].UpdateVelocity(acceleration, Universe.physicsTimeStep);
                }

                //bodies[i].UpdateVelocity (bodies, Universe.physicsTimeStep);
            }

            for (int i = 0; i < bodies.Count; i++)
            {
                bodies[i].UpdatePosition(Universe.physicsTimeStep);
            }
        }
        

    }

    public void UpdateBodies(CelestialBody[] newBodies)
    {
        foreach (CelestialBody celestial in newBodies)
        {
            bodies.Add(celestial);
        }
    }

    public void DeleteBodie(CelestialBody body)
    {
        bodies.Remove(body);
    }

    public void AddBodie(CelestialBody body)
    {
        bodies.Add(body);
    }

    public static Vector3 CalculateAcceleration (Vector3 point, CelestialBody ignoreBody = null) {
        Vector3 acceleration = Vector3.zero;
        foreach (var body in Instance.bodies) {
            if (body != ignoreBody) {
                float sqrDst = (body.Position - point).sqrMagnitude;
                Vector3 forceDir = (body.Position - point).normalized;
                acceleration += forceDir * Universe.gravitationalConstant * body.mass / sqrDst;
            }
        }

        return acceleration;
    }

    public static List<CelestialBody> Bodies {
        get {
            return Instance.bodies;
        }
    }

    static NBodySimulation Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<NBodySimulation> ();
            }
            return instance;
        }
    }
}
