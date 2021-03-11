using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public bool tutorialPassed; //not currently used, just there for in the future if needed

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) //only run the tutorial for the first level
        {
            Camera.main.GetComponent<SwitchClick>().enabled = false;
            Camera.main.GetComponent<CameraMove>().enabled = false;
            this.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
            EndTutorial();
        /*
         * if first level, show tutorial
         *    when button pressed - hide panel
         * else
         *    hide panel
        */
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
