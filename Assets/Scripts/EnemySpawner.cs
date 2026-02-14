using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    GameObject heldBoy = null;

    InputAction mouseAction;
    InputAction clickAction;

    bool firstClick = false;

    bool isBlue = true;

    public Image toggleColour;
    
    [System.Serializable]
    public struct EnemyTypes
    {
        public string type;
        public GameObject blue;
        public GameObject red;
    }

    public EnemyTypes[] enemyPrefabs;

    string currentType;

    void Start()
    {
        mouseAction = InputSystem.actions.FindAction("Point");
        clickAction = InputSystem.actions.FindAction("Click");
    }

    void Update()
    {
        if (heldBoy)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(mouseAction.ReadValue<Vector2>());
            newPos.z = 1;
            heldBoy.transform.position = newPos;

            if (clickAction.WasPerformedThisFrame() && isInBounds(newPos))
            {
                if (firstClick)
                {
                    firstClick = false;
                } else
                {
                    heldBoy = null;
                    SpawnEnemy(currentType);
                }
            }
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            isBlue = !isBlue;
            if (isBlue)
            {
                toggleColour.color = new Color(0f, 0f, 1f);
            } 
            else
            {
                toggleColour.color = new Color(1f, 0f, 0f);
            }

            SpawnEnemy(currentType);
        }
    }

    public void SpawnEnemy(string type)
    {
        EnemyTypes enemyStruct = Array.Find(enemyPrefabs, e => e.type == type);
        GameObject boy;
        if (isBlue)
        {
            boy = enemyStruct.blue;
        } else
        {
            boy = enemyStruct.red;
        }

        Destroy(heldBoy);
        heldBoy = Instantiate(boy);
        firstClick = true;
        currentType = type;
    }

    private bool isInBounds(Vector3 position)
    {
        Rect bounds = new Rect(-8, -4, 16, 8);
        return bounds.Contains(position);
    }

    public void clearHand()
    {
        heldBoy.SetActive(false);
        Destroy(heldBoy);
        heldBoy = null;
    }
}
