using UnityEngine;
using System.Collections.Generic;

public class BoysManager : MonoBehaviour
{
    private static List<GameObject> blueBoys;
    private static List<GameObject> redBoys;
    public static BoysManager Instance;

    public GameObject blueVictory;
    public GameObject redVictory;

    public GameObject resetButton;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        blueBoys = new List<GameObject>(GameObject.FindGameObjectsWithTag("Blue"));
        redBoys = new List<GameObject>(GameObject.FindGameObjectsWithTag("Red"));
    }

    public void removeBoy(GameObject boy, string type) 
    {
        if (type == "Blue")
        {
            blueBoys.Remove(boy);
        } else if (type == "Red")
        {
            redBoys.Remove(boy);
        }

        if (blueBoys.Count == 0)
        {
            redVictory.SetActive(true);
            Time.timeScale = 0f;
            resetButton.SetActive(true);
        }
        else if (redBoys.Count == 0)
        {
            blueVictory.SetActive(true);
            Time.timeScale = 0f;
            resetButton.SetActive(true);
        }
    }


    public void clearBlue()
    {
        foreach (GameObject boy in blueBoys)
        {
            Destroy(boy);
        }
    }

    public void clearRed()
    {
        foreach (GameObject boy in redBoys)
        {
            Destroy(boy);
        }
    }

    public List<GameObject> getBoysEnemies(string type) {
        if (type == "Red")
        {
            return blueBoys;
        } else if (type == "Blue")
        {
            return redBoys;
        }
        return null;
    }

    void OnDisable()
    {
        resetButton.SetActive(false);
        redVictory.SetActive(false);
        blueVictory.SetActive(false);
    }
}
