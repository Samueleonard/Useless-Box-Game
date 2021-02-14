using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public bool tutorialPassed;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.GetComponent<SwitchClick>().enabled = false;
        Camera.main.GetComponent<CameraMove>().enabled = false;
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void EndTutorial()
    {
        Camera.main.GetComponent<SwitchClick>().enabled = true;
        Camera.main.GetComponent<CameraMove>().enabled = true;
        Time.timeScale = 1;
        bool tutorialPassed = true;
        this.gameObject.SetActive(false);
    }
}
