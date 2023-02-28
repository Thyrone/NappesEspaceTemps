using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> listOfLevels = new List<GameObject>();

    public static LevelManager instance;

    GameObject currentLevel;

    public delegate void LevelChanged();
    public static event LevelChanged OnLevelChanged;
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
        currentLevel.SetActive(false);
        OnLevelChanged();
        Destroy(currentLevel); 
         currentLevel = Instantiate(listOfLevels[Random.Range(0, listOfLevels.Count)]);
    }
}
