using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject modesPanel;
    
    public void ModesClick() {
        mainPanel.SetActive(false);
        modesPanel.SetActive(true);
    }

    public void ModesBack() {
        modesPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void ModesSandbox() {
        SceneManager.LoadSceneAsync("Sandbox");
    }

}
