using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchLocation
{
    public int touchId;
    public GameObject touchPrefab;

    public touchLocation(int newTouchId, GameObject newTouchPrefab)
    {
        touchId = newTouchId;
        touchPrefab = newTouchPrefab;
    }
}
