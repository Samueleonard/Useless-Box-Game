using UnityEngine;
using UnityEngine.UI;

/*
simply works out the frame rate, and displays it onto text
*/
public class FPSCounter : MonoBehaviour
{
    public TMPro.TMP_Text fpsDisplay;

    void Update() 
    {
        fpsDisplay.text = "FPS: " + Mathf.Round(1 / Time.unscaledDeltaTime);
    }
}
