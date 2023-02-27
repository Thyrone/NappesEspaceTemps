using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    List<GameObject> listOfLevels = new List<GameObject>();

    public static LevelManager instance;

    GameObject currentLevel;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
    }

    private void Start()
    {
        if (currentLevel == null)
            currentLevel = Instantiate(listOfLevels[Random.Range(0, listOfLevels.Count)]);
        else
            ChangeLevel();
    }

    public void ChangeLevel()
    {
        Destroy(currentLevel);
        currentLevel = Instantiate(listOfLevels[Random.Range(0, listOfLevels.Count)]);
    }
}
