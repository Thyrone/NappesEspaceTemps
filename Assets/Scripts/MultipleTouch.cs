using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTouch : MonoBehaviour
{
    public GameObject prefab;
    Camera cam;
    NBodySimulation nBody;

    public List<touchLocation> touches = new List<touchLocation>();

    public delegate void PlanetCreated();
    public static event PlanetCreated OnPlanetCreated;
    void Start()
    {
        cam = GetComponent<Camera>();
        nBody = FindObjectOfType<NBodySimulation>();
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            if(t.phase==TouchPhase.Began)
            {
                touches.Add(new touchLocation(t.fingerId, CreatePrefab(t)));
                OnPlanetCreated();
            }
            else if (t.phase == TouchPhase.Ended)
            {
                touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                Destroy(thisTouch.touchPrefab);
                nBody.UpdateBodies(FindObjectsOfType<CelestialBody>());
                touches.RemoveAt(touches.IndexOf(thisTouch));
            }
            else if (t.phase == TouchPhase.Moved)
            {
                touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                thisTouch.touchPrefab.transform.position = getTouchPosition(t.position);
            }
            ++i;
        }

        Vector3 getTouchPosition(Vector2 touchPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Hit" + hit.point);
                return new Vector3(hit.point.x,0,hit.point.z);
            }
            return Vector3.zero;
            // Debug.Log(touchPosition.x + ":" + touchPosition.y);
           // return cam.ScreenToViewportPoint(new Vector3(touchPosition.x, 0, touchPosition.y));
        }

        GameObject CreatePrefab(Touch t)
        {
            GameObject go = Instantiate(prefab);
            go.name="Touch"+t.fingerId;
            Debug.Log(getTouchPosition(t.position));
            go.transform.position = getTouchPosition(t.position);
            nBody.UpdateBodies(FindObjectsOfType<CelestialBody>());
            return go;
        }
    }
}
