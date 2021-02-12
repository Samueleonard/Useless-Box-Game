using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public TMPro.TMP_Text fpsDisplay;

    private void Start() {
        Application.targetFrameRate = 100;    
    }

    void Update() 
    {
        float fps = 1 / Time.unscaledDeltaTime;
        fpsDisplay.text = "FPS: " + Mathf.Round(fps);
    }
}
