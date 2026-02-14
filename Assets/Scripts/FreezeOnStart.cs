using UnityEngine;
using System.Collections.Generic;

public class FreezeOnStart : MonoBehaviour
{
    public GameObject BoysManager;

    BoysManager boysManagerComp;

    public GameObject panel;

    public EnemySpawner enemySpawner;

    void Start()
    {
        Time.timeScale = 0f;
        enemySpawner = GetComponent<EnemySpawner>();
        boysManagerComp = BoysManager.GetComponent<BoysManager>();
    }

   public void GameStart()
   {
        enemySpawner.clearHand();
        Time.timeScale = 1f;
        BoysManager.SetActive(true);
        panel.SetActive(false);
   }

   public void GameReset()
   {
        List<GameObject> vfx = new List<GameObject>(GameObject.FindGameObjectsWithTag("VFX"));
        foreach (GameObject blood in vfx)
        {
          Destroy(blood);
        }
        
        boysManagerComp.clearBlue();
        boysManagerComp.clearRed();
        BoysManager.SetActive(false);
        panel.SetActive(true);
        Time.timeScale = 0f;
   }
}
