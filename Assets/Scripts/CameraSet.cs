using UnityEngine;

public class CameraSet : MonoBehaviour
{
    float prevHeight;
    float prevWidth;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        UpdateScreen();
    }

    void Update()
    {
        if (prevHeight != Screen.height || prevWidth != Screen.width)
        {
            UpdateScreen();
        }
    }


    void UpdateScreen()
    {
        prevWidth = (float)Screen.width;
        prevHeight = (float)Screen.height;
        
        float desiredAspect = 16f / 9f;
        float currentAspect = (float)Screen.width / (float)Screen.height;
        if (desiredAspect > currentAspect)
        {
            float heightScale = currentAspect / desiredAspect;
            Camera.main.rect = new Rect(0f, (1 - heightScale) / 2f, 1f, heightScale);
        } else {
            float widthScale = desiredAspect / currentAspect;
            Camera.main.rect = new Rect((1 - widthScale) / 2f, 0f, widthScale, 1f);
        }
    }

}
